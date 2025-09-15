using QMS.BL.DTOs.RoleDTOs;

namespace QMS.BL.Services.Interfaces;

public interface IRoleService
{
    Task<Result<IEnumerable<RoleDTO>>> GetAllRolesAsync();
    Task<Result<RoleDTO>> GetRoleByIdAsync(byte id);
    Task<Result<RoleDTO>> CreateRoleAsync(CreateRoleDTO dto, string userId);
    Task<Result<RoleDTO>> UpdateRoleAsync(byte id,UpdateRoleDTO dto, string userId);
    Task<Result<RoleDTO>> DeleteRoleAsync(byte id);

}
