using QMS.BL.DTOs.CompanyDTOs;

namespace QMS.BL.Services.Implementations;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyService> _logger;

    private readonly string[] _includes = new[] { "Branchs", "Subscriptions" };

    public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompanyService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<CompanyDTO>> CreateCompanyAsync(CreateCompanyDTO dto, string userId)
    {
        _logger.LogInformation("Starting company creation process.");
        _logger.LogDebug("Incoming company data {@CompanyData}", dto);


        try
        {
            _logger.LogDebug("Mapping DTO to Company entity");
            var company = _mapper.Map<Company>(dto);

            company.CreatedBy = userId;
            company.ModifiedBy = userId;


            _logger.LogDebug("Add new company to database.");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created company `{company.Name}` with ID `{company.CompanyID}`");

            return Result<CompanyDTO>.Success( _mapper.Map<CompanyDTO>(company),StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error while creating company `{dto.Name}`");
            return Result<CompanyDTO>.Failure("Failed to save company due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating company `{dto.Name}`");
            return Result<CompanyDTO>.Failure("Failed to process company data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Create company");
            return Result<CompanyDTO>.Failure("An error occurred while processing your request",StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<IEnumerable<CompanyDTO>>> GetAllCompaniesAsync()
    {
        try
        {
            var orderBy = new List<(Expression<Func<Company, object>> keySelector, bool ascending)>
            {
                (c => c.Name, true)
            };

            var companies = await _unitOfWork.Companies.GetAllAsync(includes: _includes, orderBy: orderBy);

            if (!companies.Any())
            {
                _logger.LogWarning("No movies found.");
                return Result<IEnumerable<CompanyDTO>>.Failure("No companies found", 204); 
            }

            var CompanyDto = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return Result<IEnumerable<CompanyDTO>>.Success(CompanyDto, 200);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving company data.");
            return Result<IEnumerable<CompanyDTO>>.Failure("Data mapping error", 500);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving companies.");
            return Result<IEnumerable<CompanyDTO>>.Failure("Unable to retrieve companies at this time", 503);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unexpected error retrieving companies.");
            return Result<IEnumerable<CompanyDTO>>.Failure("Unexpected error", 500);
        }
    }

    public async Task<Result<CompanyDTO>> GetCompanyByIdAsync(int id)
    {
        _logger.LogInformation($"Starting company retrieval for ID {id}");

        try
        {
            _logger.LogDebug($"Quering database for company {id}");

            var stopwatch = Stopwatch.StartNew();
            var company = await _unitOfWork.Companies.GetByIdAsync(id, _includes);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (company is null)
            {
                _logger.LogWarning($"Company not found for ID {id}");
                return Result<CompanyDTO>.Failure($"Company with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successfully retrieved genre `{company.Name}` (ID: {company.CompanyID})");

            return Result<CompanyDTO>.Success(_mapper.Map<CompanyDTO>(company));
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving company data.");

            return Result<CompanyDTO>.Failure("An error occurred while mapping company data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, $"Database error while retrieving company");
            return Result<CompanyDTO>.Failure("Unable to retrieve company at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting company with ID {id}");
            return Result<CompanyDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<CompanyDTO>> UpdateCompanyAsync(int id, UpdateCompanyDTO dto,string userId)
    {
        try
        {

            _logger.LogDebug($"Retrieving company `{id}` for update");

            var company = await _unitOfWork.Companies.GetByIdAsync(id, _includes);

            if (company is null)
            {
                _logger.LogWarning($"Company not found for update: {id}");
                return Result<CompanyDTO>.Failure($"Company with ID `{id}` not found",StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Company entity");
            _mapper.Map(dto, company);

            company.ModifiedBy = userId;

            _logger.LogInformation($"Updating company `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Companies.Update(company);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<CompanyDTO>.Success(_mapper.Map<CompanyDTO>(company),StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating company {id}");
            return Result<CompanyDTO>.Failure("The company was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating company {id}");
            return Result<CompanyDTO>.Failure("Failed to update company due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating company {id}");
            return Result<CompanyDTO>.Failure("Failed to process company data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update company");
            return Result<CompanyDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }
    public async Task<Result<CompanyDTO>> DeleteCompanyAsync(int id)
    {
        _logger.LogInformation($"Starting company deletion for ID {id}");
        try
        {
            _logger.LogDebug($"Fetching company with ID {id}");
            var company = await _unitOfWork.Companies.GetByIdAsync(id, _includes);

            if (company is null)
            {
                _logger.LogWarning($"Company not found for deletion: {id}");
                return Result<CompanyDTO>.Failure($"No company found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting company `{company.Name}` (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Companies.Delete(company);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");
            return Result<CompanyDTO>.Success(_mapper.Map<CompanyDTO>(company),StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting company {id}");
            return Result<CompanyDTO>.Failure("An error occurred while deleting the company", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting genre {id}");
            return Result<CompanyDTO>.Failure("The company was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting company {id}");
            return Result<CompanyDTO>.Failure("Failed to delete company due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting company with ID {id}");
            return Result<CompanyDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }
}
