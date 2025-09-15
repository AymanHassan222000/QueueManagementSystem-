using QMS.BL.DTOs.BranchDTOs;

namespace QMS.BL.Services.Implementations;

public class BranchService : IBranchService
{
    private readonly IMapper _mapper;
    private readonly ILogger<BranchService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string[] _includes = new[] { "Queues","Users"};

    public BranchService(IMapper mapper,
                          ILogger<BranchService> logger,
                          IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BranchDTO>> CreateBranchAsync(CreateBranchDTO dto, string userId)
    {
        try
        {
            _logger.LogDebug("Mapping DTO to Branch entity");
            var branch = _mapper.Map<Branch>(dto);

            branch.CreatedBy = userId;
            branch.ModifiedBy = userId;

            _logger.LogDebug("Add new branch to database");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Branches.AddAsync(branch);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created branch `{branch.Name}` with ID `{branch.BranchID}`");
            return Result<BranchDTO>.Success(_mapper.Map<BranchDTO>(branch), StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error while creating branch `{dto.Name}`");
            return Result<BranchDTO>.Failure("Failed to save branch due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating branch `{dto.Name}`");
            return Result<BranchDTO>.Failure("Failed to process branch data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Create Branch");
            return Result<BranchDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IEnumerable<BranchDTO>>> GetAllBranchesAsync()
    {
        _logger.LogInformation("Starting retrieval of all branches.");

        try
        {
            var orderBy = new List<(Expression<Func<Branch, object>> keySlector, bool ascending)>
            {
                (b => b.Name,true)
            };

            _logger.LogDebug("Querying database for branches");

            var stopwatch = Stopwatch.StartNew();
            var branches = await _unitOfWork.Branches.GetAllAsync(includes: _includes,orderBy: orderBy);
            stopwatch.Stop();
            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (!branches.Any())
            {
                _logger.LogInformation("No branches found in database");
                return Result<IEnumerable<BranchDTO>>.Failure("No branches found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Retrieved {branches.Count()} branches");
            var branchDto = _mapper.Map<IEnumerable<BranchDTO>>(branches);

            return Result<IEnumerable<BranchDTO>>.Success(branchDto, StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving branchs data.");
            return Result<IEnumerable<BranchDTO>>.Failure("An error occurred while mapping branch data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving branches");
            return Result<IEnumerable<BranchDTO>>.Failure("Unable to retrieve branches at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting All Branches");
            return Result<IEnumerable<BranchDTO>>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<BranchDTO>> GetBranchByIdAsync(int id)
    {
        _logger.LogInformation($"Starting branch retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Qeurying database for branch {id}");

            var stopwatch = Stopwatch.StartNew();
            var branch = await _unitOfWork.Branches.GetByIdAsync(id,_includes);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (branch is null)
            {
                _logger.LogWarning($"Branch not found for ID {id}");
                return Result<BranchDTO>.Failure($"Branch with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successful retrieval branch `{branch.Name}` (ID: {branch.BranchID})");

            return Result<BranchDTO>.Success(_mapper.Map<BranchDTO>(branch), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving branch data.");
            return Result<BranchDTO>.Failure("An error occurred while mapping branch data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving branch");
            return Result<BranchDTO>.Failure("Unable to retrieve branch at this time", StatusCodes.Status503ServiceUnavailable);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting Branch with ID {id}");
            return Result<BranchDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<BranchDTO>> UpdateBranchAsync(int id, UpdateBranchDTO dto, string userId)
    {
        _logger.LogInformation($"Starting branch update for ID {id}.");
        _logger.LogDebug("Incoming branch data {@BranchData}", dto);

        try
        {
            _logger.LogDebug($"Retrieving branch `{id}` for update");

            var branch = await _unitOfWork.Branches.GetByIdAsync(id,_includes);

            if (branch is null)
            {
                _logger.LogWarning($"Branch not found for update: {id}");
                return Result<BranchDTO>.Failure($"Branch with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Branch entity");
            _mapper.Map(dto, branch);
            branch.ModifiedBy = userId;

            _logger.LogInformation($"Updating branch `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Branches.Update(branch);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<BranchDTO>.Success(_mapper.Map<BranchDTO>(branch), StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating branch {id}");
            return Result<BranchDTO>.Failure("The branch was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating branch {id}");
            return Result<BranchDTO>.Failure($"Failed to update branch due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating branch {id}");
            return Result<BranchDTO>.Failure("Failed to process branch data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update branch");
            return Result<BranchDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }
    public async Task<Result<BranchDTO>> DeleteBranchAsync(int id)
    {
        _logger.LogInformation($"Starting branch deletion for ID {id}");
        try
        {
            _logger.LogDebug($"Fetching branch with ID {id}");
            var branch = await _unitOfWork.Branches.GetByIdAsync(id);

            if (branch is null)
            {
                _logger.LogWarning($"Branch not found for deletion: {id}");
                return Result<BranchDTO>.Failure($"No branch found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting branch `{branch.Name}` (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Branches.Delete(branch);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<BranchDTO>.Success(_mapper.Map<BranchDTO>(branch),StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting branch {id}");
            return Result<BranchDTO>.Failure("An error occurred while deleting the branch", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting branch {id}");
            return Result<BranchDTO>.Failure("The branch was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting branch {id}");
            return Result<BranchDTO>.Failure("Failed to delete branch due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting branch with ID {id}");
            return Result<BranchDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
}
