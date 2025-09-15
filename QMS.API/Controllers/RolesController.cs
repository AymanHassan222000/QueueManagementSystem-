using QMS.BL.DTOs.RoleDTOs;
using QMS.DAL.Models;

namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var result = await _roleService.GetAllRolesAsync();

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleByIdAsync(byte id)
    {
        var result = await _roleService.GetRoleByIdAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(CreateRoleDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";
        var result = await _roleService.CreateRoleAsync(dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError ,result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoleAsync(byte id, [FromBody] UpdateRoleDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";
        var result = await _roleService.UpdateRoleAsync(id,dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> DeleteRoleAsync(byte id)
    {
        var result = await _roleService.DeleteRoleAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);

    }

}
