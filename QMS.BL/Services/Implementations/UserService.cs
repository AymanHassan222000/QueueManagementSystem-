using QMS.BL.DTOs.BranchDTOs;
using QMS.BL.DTOs.UserDTOs;
using QMS.DAL.Enums;

namespace QMS.BL.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private string[] _includes = new[] { "UserRoles" };
    public UserService(IUnitOfWork unitOfWork,
                       ILogger<UserService> logger,
                       IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<UserDTO>> CreateUserAsync(CreateUserDTO dto, string userId)
    {
        try
        {
            _logger.LogDebug("Mapping DTO to User entity");

            var user = await _unitOfWork.Users.FindAsync(e => e.UserName == dto.UserName, null);

            if (user != null)
                return Result<UserDTO>.Failure("This username already exists.", StatusCodes.Status400BadRequest);

            user = _mapper.Map<User>(dto);

            user.CreatedBy = userId;
            user.ModifiedBy = userId;
            Branch? branch = null;
            if (user.BranchID != null)
            {
                branch = await _unitOfWork.Branches.FindAsync(e => e.BranchID == dto.BranchID, null);
                if (branch is null)
                    return Result<UserDTO>.Failure($"No branches was found with ID {dto.BranchID}", StatusCodes.Status400BadRequest);
            }
            user.UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    BranchID = user.BranchID,
                    CompanyID = branch?.CompanyID ,
                    RoleID = (int)UserRoles.Customer,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                }
            };

            if (dto.IdentityIDs.Count != 0 && dto.IdentityTypeIDs.Count != 0)
            {
                var existingTypeIds = (await _unitOfWork.IdentityTypes.GetAllAsync())
                                      .Select(x => x.IdentityTypeID);

                var invalidIds = dto.IdentityTypeIDs.Except(existingTypeIds).ToList();

                if (invalidIds.Any())
                {
                    return  Result<UserDTO>.Failure($"Some IdentityTypeIDs are invalid: {string.Join(", ", invalidIds)}", StatusCodes.Status400BadRequest);
                }

                user.UserIdentities = dto.IdentityTypeIDs
                    .Select((typeId, index) => new UserIdentity
                    {
                        CreatedBy = userId,
                        ModifiedBy = userId,
                        IdentityTypeID = typeId,
                        IdentityID = dto.IdentityIDs[index] 
                    }).ToList();
            }

            _logger.LogDebug("Add new user to database");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created user with ID `{user.BranchID}`");
            return Result<UserDTO>.Success(_mapper.Map<UserDTO>(user), StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex);
            _logger.LogError(ex, $"Database error while creating user `{dto.Email}`");
            return Result<UserDTO>.Failure("Failed to save user due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating user `{dto.Email}`");
            return Result<UserDTO>.Failure("Failed to process user data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            _logger.LogError(ex, "Error Create Branch");
            return Result<UserDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IEnumerable<UserDTO>>> GetAllUsersAsync()
    {
        _logger.LogInformation("Starting retrieval of all users.");

        try
        {
            var orderBy = new List<(Expression<Func<User, object>> keySlector, bool ascending)>
            {
                (b => b.FirstName,true),
                (b => b.LastName,true),
            };

            _logger.LogDebug("Querying database for users");

            var stopwatch = Stopwatch.StartNew();
            var branches = await _unitOfWork.Users.GetAllAsync(includes: _includes, orderBy: orderBy);
            stopwatch.Stop();
            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (!branches.Any())
            {
                _logger.LogInformation("No branches found in database");
                return Result<IEnumerable<UserDTO>>.Failure("No users found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Retrieved {branches.Count()} users");
            var userDto = _mapper.Map<IEnumerable<UserDTO>>(branches);

            return Result<IEnumerable<UserDTO>>.Success(userDto, StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving users data.");
            return Result<IEnumerable<UserDTO>>.Failure("An error occurred while mapping user data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving users");
            return Result<IEnumerable<UserDTO>>.Failure("Unable to retrieve users at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting All users");
            return Result<IEnumerable<UserDTO>>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<UserDTO>> GetUserByIdAsync(int id)
    {
        _logger.LogInformation($"Starting user retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Qeurying database for user {id}");

            var stopwatch = Stopwatch.StartNew();
            var user = await _unitOfWork.Users.GetByIdAsync(id, _includes);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (user is null)
            {
                _logger.LogWarning($"User not found for ID {id}");
                return Result<UserDTO>.Failure($"User with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successful retrieval User (ID: {user.UserID})");

            return Result<UserDTO>.Success(_mapper.Map<UserDTO>(user), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving user data.");
            return Result<UserDTO>.Failure("An error occurred while mapping user data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving user");
            return Result<UserDTO>.Failure("Unable to retrieve user at this time", StatusCodes.Status503ServiceUnavailable);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting user with ID {id}");
            return Result<UserDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<UserDTO>> UpdateUserAsync(int id, UpdateUserDTO dto, string userId)
    {
        _logger.LogInformation($"Starting user update for ID {id}.");
        _logger.LogDebug("Incoming user data {@BranchData}", dto);

        try
        {
            _logger.LogDebug($"Retrieving user `{id}` for update");

            var user = await _unitOfWork.Users.GetByIdAsync(id, _includes);

            if (user is null)
            {
                _logger.LogWarning($"User not found for update: {id}");
                return Result<UserDTO>.Failure($"User with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to user entity");
            _mapper.Map(dto, user);
            user.ModifiedBy = userId;

            _logger.LogInformation($"Updating user `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<UserDTO>.Success(_mapper.Map<UserDTO>(user), StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating user {id}");
            return Result<UserDTO>.Failure("The user was modified by another one. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating user {id}");
            return Result<UserDTO>.Failure($"Failed to update user due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating user {id}");
            return Result<UserDTO>.Failure("Failed to process user data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update user");
            return Result<UserDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<UserDTO>> DeleteUserAsync(int id)
    {
        _logger.LogInformation($"Starting user deletion for ID {id}");
        try
        {
            _logger.LogDebug($"Fetching user with ID {id}");
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogWarning($"user not found for deletion: {id}");
                return Result<UserDTO>.Failure($"No user found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting user (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            user.IsDeleted = true;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<UserDTO>.Success(_mapper.Map<UserDTO>(user), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting user {id}");
            return Result<UserDTO>.Failure("An error occurred while deleting the user", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting user {id}");
            return Result<UserDTO>.Failure("The user was deleted by another one. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting user {id}");
            return Result<UserDTO>.Failure("Failed to delete user due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting user with ID {id}");
            return Result<UserDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
}
