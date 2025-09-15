using QMS.BL.DTOs.BranchDTOs;
using QMS.BL.Results;

namespace QMS.BL.Services.Interfaces;

public interface IBranchService
{
    Task<Result<BranchDTO>> CreateBranchAsync(CreateBranchDTO dto,string userId);
    Task<Result<IEnumerable<BranchDTO>>> GetAllBranchesAsync();
    Task<Result<BranchDTO>> GetBranchByIdAsync(int id);
    Task<Result<BranchDTO>> UpdateBranchAsync(int id, UpdateBranchDTO dto,string userId);
    Task<Result<BranchDTO>> DeleteBranchAsync(int id);
}
