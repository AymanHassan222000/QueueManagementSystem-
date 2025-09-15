using QMS.BL.DTOs.CompanyDTOs;
using QMS.BL.Results;

namespace QMS.BL.Services.Interfaces;

public interface ICompanyService
{
    Task<Result<IEnumerable<CompanyDTO>>> GetAllCompaniesAsync();
    Task<Result<CompanyDTO>> GetCompanyByIdAsync(int id);
    Task<Result<CompanyDTO>> CreateCompanyAsync(CreateCompanyDTO dto, string userId);
    Task<Result<CompanyDTO>> UpdateCompanyAsync(int id, UpdateCompanyDTO dto, string userId);
    Task<Result<CompanyDTO>> DeleteCompanyAsync(int id);

}
