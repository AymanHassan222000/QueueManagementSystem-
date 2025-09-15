using QMS.BL.DTOs.BranchDTOs;
using QMS.BL.DTOs.IdentityTypesDTOs;

namespace QMS.BL.Services.Implementations;

public class IdentityTypeService : IIdentityTypeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<IdentityTypeService> _logger;

    public IdentityTypeService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<IdentityTypeService> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IdentityTypeResponseDTO>> CreateIdentityTypeAsync(IdentityTypeRequestDTO dto, string userId)
    {
        try
        {
            var identiy = await _unitOfWork.IdentityTypes.FindAsync(e => e.Name == dto.Name,null);
            
            if (identiy != null)
                return Result<IdentityTypeResponseDTO>.Failure("This Identity already exists.", StatusCodes.Status400BadRequest);
            
            _logger.LogDebug("Mapping DTO to identity type entity");
            var identityType = _mapper.Map<IdentityType>(dto);

            identityType.CreatedBy = userId;
            identityType.ModifiedBy = userId;

            _logger.LogDebug("Add new identity type to database");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.IdentityTypes.AddAsync(identityType);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created identity type `{identityType.Name}` with ID `{identityType.IdentityTypeID}`");
            return Result<IdentityTypeResponseDTO>.Success(_mapper.Map<IdentityTypeResponseDTO>(identityType), StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex);
            _logger.LogError(ex, $"Database error while creating identity type `{dto.Name}`");
            return Result<IdentityTypeResponseDTO>.Failure("Failed to save identity type due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating identity type `{dto.Name}`");
            return Result<IdentityTypeResponseDTO>.Failure("Failed to process identity type data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Create Branch");
            return Result<IdentityTypeResponseDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }


    public async Task<Result<IEnumerable<IdentityTypeResponseDTO>>> GetAllIdentityTypesAsync()
    {
        _logger.LogInformation("Starting retrieval of all identity type.");

        try
        {
            _logger.LogDebug("Querying database for identity type");

            var stopwatch = Stopwatch.StartNew();
            var identityTypes = await _unitOfWork.IdentityTypes.GetAllAsync();
            stopwatch.Stop();
            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (!identityTypes.Any())
            {
                _logger.LogInformation("No identity types found in database");
                return Result<IEnumerable<IdentityTypeResponseDTO>>.Failure("No identity type found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Retrieved {identityTypes.Count()} identity types");
            var idntityTypeDto = _mapper.Map<IEnumerable<IdentityTypeResponseDTO>>(identityTypes);

            return Result<IEnumerable<IdentityTypeResponseDTO>>.Success(idntityTypeDto, StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving identity type data.");
            return Result<IEnumerable<IdentityTypeResponseDTO>>.Failure("An error occurred while mapping identity type data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving idnetity type");
            return Result<IEnumerable<IdentityTypeResponseDTO>>.Failure("Unable to retrieve identity type at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting All identity type");
            return Result<IEnumerable<IdentityTypeResponseDTO>>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IdentityTypeResponseDTO>> GetIdentityTypeByIdAsync(byte id)
    {
        _logger.LogInformation($"Starting identity type retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Qeurying database for identity type {id}");

            var stopwatch = Stopwatch.StartNew();
            var branch = await _unitOfWork.IdentityTypes.GetByIdAsync(id);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (branch is null)
            {
                _logger.LogWarning($"Identity type not found for ID {id}");
                return Result<IdentityTypeResponseDTO>.Failure($"Identity type with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successful retrieval identity type `{branch.Name}` (ID: {branch.IdentityTypeID})");

            return Result<IdentityTypeResponseDTO>.Success(_mapper.Map<IdentityTypeResponseDTO>(branch), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving identity type data.");
            return Result<IdentityTypeResponseDTO>.Failure("An error occurred while mapping identity type data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving identity type");
            return Result<IdentityTypeResponseDTO>.Failure("Unable to retrieve identity type at this time", StatusCodes.Status503ServiceUnavailable);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting identity type with ID {id}");
            return Result<IdentityTypeResponseDTO>.Failure("An error occurred while mapping identity data.", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IdentityTypeResponseDTO>> UpdateIdentityTypeAsync(byte id, IdentityTypeRequestDTO dto, string userId)
    {
        _logger.LogInformation($"Starting identity type update for ID {id}.");
        _logger.LogDebug("Incoming branch data {@BranchData}", dto);

        try
        {
            var identiy = await _unitOfWork.IdentityTypes.FindAsync(e => e.Name == dto.Name, null);

            if (identiy != null)
                return Result<IdentityTypeResponseDTO>.Failure("This Identity already exists.", StatusCodes.Status400BadRequest);

            _logger.LogDebug($"Retrieving identity type `{id}` for update");

            var identityType = await _unitOfWork.IdentityTypes.GetByIdAsync(id);

            if (identityType is null)
            {
                _logger.LogWarning($"Identity Type not found for update: {id}");
                return Result<IdentityTypeResponseDTO>.Failure($"Identity Type with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Identity Type entity");
            _mapper.Map(dto, identityType);
            identityType.ModifiedBy = userId;

            _logger.LogInformation($"Updating Identity Type `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.IdentityTypes.Update(identityType);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<IdentityTypeResponseDTO>.Success(_mapper.Map<IdentityTypeResponseDTO>(identityType), StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating identity Type {id}");
            return Result<IdentityTypeResponseDTO>.Failure("The idenity type was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating identity type {id}");
            return Result<IdentityTypeResponseDTO>.Failure($"Failed to update identity type due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating identiy type {id}");
            return Result<IdentityTypeResponseDTO>.Failure("Failed to process identity type data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update identity type");
            return Result<IdentityTypeResponseDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
    public async Task<Result<IdentityTypeResponseDTO>> DeleteIdentityTypeAsync(byte id)
    {
        _logger.LogInformation($"Starting identity type deletion for ID {id}");
        try
        {
            _logger.LogDebug($"Fetching identity type with ID {id}");
            var identityType = await _unitOfWork.IdentityTypes.GetByIdAsync(id);

            if (identityType is null)
            {
                _logger.LogWarning($"Identity type not found for deletion: {id}");
                return Result<IdentityTypeResponseDTO>.Failure($"No Identity type found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting identity type `{identityType.Name}` (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.IdentityTypes.Delete(identityType);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<IdentityTypeResponseDTO>.Success(_mapper.Map<IdentityTypeResponseDTO>(identityType), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting identity type {id}");
            return Result<IdentityTypeResponseDTO>.Failure("An error occurred while deleting the identity type", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting identity type {id}");
            return Result<IdentityTypeResponseDTO>.Failure("The identity type was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting identity type {id}");
            return Result<IdentityTypeResponseDTO>.Failure("Failed to delete identity type due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting identity type with ID {id}");
            return Result<IdentityTypeResponseDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
}
