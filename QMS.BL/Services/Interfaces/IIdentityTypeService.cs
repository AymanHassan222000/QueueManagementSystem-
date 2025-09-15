using QMS.BL.DTOs.IdentityTypesDTOs;

namespace QMS.BL.Services.Interfaces;

public interface IIdentityTypeService
{
    Task<Result<IdentityTypeResponseDTO>> CreateIdentityTypeAsync(IdentityTypeRequestDTO dto, string userId);
    Task<Result<IEnumerable<IdentityTypeResponseDTO>>> GetAllIdentityTypesAsync();
    Task<Result<IdentityTypeResponseDTO>> GetIdentityTypeByIdAsync(byte id);
    Task<Result<IdentityTypeResponseDTO>> UpdateIdentityTypeAsync(byte id, IdentityTypeRequestDTO dto, string userId);
    Task<Result<IdentityTypeResponseDTO>> DeleteIdentityTypeAsync(byte id);
}
