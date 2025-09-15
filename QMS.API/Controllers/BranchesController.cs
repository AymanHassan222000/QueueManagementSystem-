using QMS.BL.DTOs.BranchDTOs;

namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchServices;
    public BranchesController(IBranchService branchServices)
    {
        _branchServices = branchServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBranchesAsync()
    {
        var result = await _branchServices.GetAllBranchesAsync();

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchByIdAsync(int id)
    {
        var result = await _branchServices.GetBranchByIdAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);

    }

    [HttpPost]
    public async Task<IActionResult> CreateBranchAsync(CreateBranchDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _branchServices.CreateBranchAsync(dto,userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranchAsync(int id, [FromBody] UpdateBranchDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _branchServices.UpdateBranchAsync(id,dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranchAsync(int id)
    {
        var result = await _branchServices.DeleteBranchAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }
}
