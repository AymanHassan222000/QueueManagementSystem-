using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserIdentitiesController : ControllerBase
{
	//private readonly IBaseRepository<UserIdentity> _userIdentityRepository;
	//private readonly IMapper _mapper;
	//public UserIdentitiesController(IBaseRepository<UserIdentity> userIdentityRepository, IMapper mapper)
	//{
	//	_userIdentityRepository = userIdentityRepository;
	//	_mapper = mapper;
	//}

	//[HttpGet("GetAll")]
	//public async Task<IActionResult> GetAllAsync()
	//{
	//	return Ok(await _userIdentityRepository.GetAll());
	//}

	//[HttpGet("GetById/{id}")]
	//public async Task<IActionResult> GetByIdAsync(int id)
	//{
	//	var userIdentity = await _userIdentityRepository.GetById(id);

	//	if (userIdentity is null)
	//		return NotFound();

	//	return Ok(userIdentity);
	//}

	//[HttpPost("Create")]
	//public async Task<IActionResult> CreateAsync(UserIdentityDTO dto)
	//{
	//	var userIdentity = _mapper.Map<UserIdentity>(dto);

	//	await _userIdentityRepository.Add(userIdentity);

	//	return Ok(userIdentity);
	//}

	//[HttpPut("Update/{id}")]
	//public async Task<IActionResult> Update(int id, [FromBody] UserIdentityDTO dto)
	//{
	//	var userIdentity = await _userIdentityRepository.GetById(id);

	//	if (userIdentity is null)
	//		return NotFound();

	//	_mapper.Map(dto, userIdentity);
	//	_userIdentityRepository.Update(userIdentity);

	//	return Ok(userIdentity);
	//}

	//[HttpPut("Delete/{id}")]
	//public async Task<IActionResult> Delete(int id)
	//{
	//	var userIdentity = await _userIdentityRepository.GetById(id);

	//	if (userIdentity is null)
	//		return NotFound();

	//	_userIdentityRepository.Delete(userIdentity);

	//	return Ok(userIdentity);
	//}
}
