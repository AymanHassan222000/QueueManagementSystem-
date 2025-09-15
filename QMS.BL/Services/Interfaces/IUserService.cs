using QMS.BL.DTOs.UserDTOs;

namespace QMS.BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDTO>> CreateUserAsync(CreateUserDTO dto, string userId);
        Task<Result<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<Result<UserDTO>> GetUserByIdAsync(int id);
        Task<Result<UserDTO>> UpdateUserAsync(int id, UpdateUserDTO dto, string userId);
        Task<Result<UserDTO>> DeleteUserAsync(int id);

    }
}
