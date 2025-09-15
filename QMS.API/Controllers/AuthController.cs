using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//	private readonly IAuthService _authService;

//	public AuthController(IAuthService authService)
//	{
//		_authService = authService;
//	}

//	[HttpPost("Register")]
//	public async Task<IActionResult> RegisterAsync(UserDTO dto)
//	{
//		if (!ModelState.IsValid)
//			return BadRequest(ModelState);

//		var result = await _authService.RegisterAsync(dto);

//		if (!result.IsAuthenticated)
//			return BadRequest(result.Message);

//		return Ok(result);
//	}

//	[HttpPost("GetToken")]
//	public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
//	{
//		if (!ModelState.IsValid)
//			return BadRequest(ModelState);

//		var result = await _authService.GetTokenAsync(model);

//		if (!result.IsAuthenticated)
//			return BadRequest(result.Message);

//		return Ok(result);
//	}

//}
