using QMS.BL.DTOs.RoleDTOs;
using QMS.BL.Results;

namespace QMS.BL.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleService> _logger;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork,
                       ILogger<RoleService> logger,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<RoleDTO>> CreateRoleAsync(CreateRoleDTO dto, string userId)
    {
        _logger.LogInformation("Starting role creation process.");

        try
        {
            _logger.LogDebug("Mapping DTO to role entity");
            var role = _mapper.Map<Role>(dto);

            role.CreatedBy = userId;
            role.ModifiedBy = userId;

            _logger.LogDebug("Add new role to database.");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");
            _logger.LogInformation($"Successful created role `{role.Name}` with ID `{role.RoleID}`");

            return Result<RoleDTO>.Success(_mapper.Map<RoleDTO>(role),StatusCodes.Status201Created); ;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error while creating role `{dto.Name}`");
            return Result<RoleDTO>.Failure("Failed to save role due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating role `{dto.Name}`");
            return Result<RoleDTO>.Failure("Failed to process role data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Create role");
            return Result<RoleDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<RoleDTO>> DeleteRoleAsync(byte id)
    {
        _logger.LogInformation($"Starting role deletion for ID {id}");

        try
        {
            _logger.LogDebug($"Fetching role with ID {id}");
            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role is null)
            {
                _logger.LogWarning($"Role not found for deletion: {id}");
                return Result<RoleDTO>.Failure($"No role found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting role `{role.Name}` (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Roles.Delete(role);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");
            return Result<RoleDTO>.Success(_mapper.Map<RoleDTO>(role),StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting role {id}");
            return Result<RoleDTO>.Failure("An error occurred while deleting the role", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting role {id}");
            return Result<RoleDTO>.Failure("The role was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting role {id}");
            return Result<RoleDTO>.Failure("Failed to delete role due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting role with ID {id}");
            return Result<RoleDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IEnumerable<RoleDTO>>> GetAllRolesAsync()
    {
        _logger.LogInformation("Starting retrievel of all roles");
        try
        {
            _logger.LogDebug("Querying database for roles");

            var stopwatch = Stopwatch.StartNew();
            var roles = await _unitOfWork.Roles.GetAllAsync();
            stopwatch.Stop();

            _logger.LogInformation($"Retrieved {roles.Count()} companies");

            if (!roles.Any())
            {
                _logger.LogWarning("No roles found in database");
                return Result<IEnumerable<RoleDTO>>.Failure("No roles was found",StatusCodes.Status404NotFound);
            }

            return Result<IEnumerable<RoleDTO>>.Success(_mapper.Map<IEnumerable<RoleDTO>>(roles),StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving role data.");
            return Result<IEnumerable<RoleDTO>>.Failure("An error occurred while mapping role data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving roles");
            return Result<IEnumerable<RoleDTO>>.Failure("Unable to retrieve roles at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unexpected error retrieving roles");
            return Result<IEnumerable<RoleDTO>>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<RoleDTO>> GetRoleByIdAsync(byte id)
    {
        _logger.LogInformation($"Starting role retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Quering database for role {id}");

            var stopwatch = Stopwatch.StartNew();
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            stopwatch.Stop();

            if (role is null)
            {
                _logger.LogWarning($"Role not found for ID {id}");
                return Result<RoleDTO>.Failure($"Role with ID {id} not found", StatusCodes.Status404NotFound);
            }
            _logger.LogInformation($"Successfully retrieved role `{role.Name}` (ID: {role.RoleID})");

            return Result<RoleDTO>.Success(_mapper.Map<RoleDTO>(role),StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving role data.");
            return Result<RoleDTO>.Failure("An error occurred while mapping role data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, $"Database error while retrieving role");
            return Result<RoleDTO>.Failure("Unable to retrieve role at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting role with ID {id}");
            return Result<RoleDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<RoleDTO>> UpdateRoleAsync(byte id, UpdateRoleDTO dto, string userId)
    {
        _logger.LogInformation($"Starting role update for ID {id}.");

        try
        {
            _logger.LogDebug($"Retrieving role `{id}` for update");

            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role is null)
            {
                _logger.LogWarning($"Role not found for update: {id}");
                return Result<RoleDTO>.Failure($"Role with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Role entity");
            _mapper.Map(dto, role);

            role.ModifiedBy = userId;

            _logger.LogInformation($"Updating Role `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Roles.Update(role);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<RoleDTO>.Success(_mapper.Map<RoleDTO>(role),StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating role {id}");
            return Result<RoleDTO>.Failure("The role was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating role {id}");
            return Result<RoleDTO>.Failure("Failed to update role due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating role {id}");
            return Result<RoleDTO>.Failure("Failed to process role data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update role");
            return Result<RoleDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
}
