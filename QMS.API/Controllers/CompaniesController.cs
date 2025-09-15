using QMS.BL.DTOs.CompanyDTOs;


namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class CompaniesController : ControllerBase
{

    private readonly ICompanyService _companyService;
    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompanyAsync(CreateCompanyDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _companyService.CreateCompanyAsync(dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompaniesAsync()
    {
        var result = await _companyService.GetAllCompaniesAsync();

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyByIdAsync(int id)
    {
        var result = await _companyService.GetCompanyByIdAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompanyAsync(int id, [FromBody] UpdateCompanyDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _companyService.UpdateCompanyAsync(id, dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompanyAsync(int id)
    {
        var result = await _companyService.DeleteCompanyAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }
}
