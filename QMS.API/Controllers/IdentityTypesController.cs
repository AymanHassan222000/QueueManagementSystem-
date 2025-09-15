using QMS.BL.DTOs.BranchDTOs;
using QMS.BL.DTOs.IdentityTypesDTOs;
using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class IdentityTypesController : ControllerBase
	{
		private readonly IIdentityTypeService _identityTypeService;

        public IdentityTypesController(IIdentityTypeService identityTypeService)
        {
            _identityTypeService = identityTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIdentityTypesAsync()
        {
            var result = await _identityTypeService.GetAllIdentityTypesAsync();

            if (result.IsFailure)
                return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

            return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdentityTypeByIdAsync(byte id)
        {
            var result = await _identityTypeService.GetIdentityTypeByIdAsync(id);

            if (result.IsFailure)
                return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

            return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);

        }

        [HttpPost]
        public async Task<IActionResult> CreateIdentityTypeAsync(IdentityTypeRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return this.HandleInvalidModelState();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

            var result = await _identityTypeService.CreateIdentityTypeAsync(dto, userId);

            if (result.IsFailure)
                return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

            return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdentityTypeAsync(byte id, [FromBody] IdentityTypeRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return this.HandleInvalidModelState();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

            var result = await _identityTypeService.UpdateIdentityTypeAsync(id, dto, userId);

            if (result.IsFailure)
                return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

            return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdentityTypeAsync(byte id)
        {
            var result = await _identityTypeService.DeleteIdentityTypeAsync(id);

            if (result.IsFailure)
                return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

            return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
        }

    }
}
