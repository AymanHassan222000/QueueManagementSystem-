using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserQueuesController : ControllerBase
{
	//private readonly IBaseRepository<UserQueue> _userQueueRepository;
	//private readonly IMapper _mapper;
	//public UserQueuesController(IBaseRepository<UserQueue> userQueueRepository, IMapper mapper)
	//{
	//	_userQueueRepository = userQueueRepository;
	//	_mapper = mapper;
	//}

	//[HttpGet("GetAll")]
	//public async Task<IActionResult> GetAllAsync()
	//{
	//	return Ok(await _userQueueRepository.GetAll());
	//}

	//[HttpGet("GetById/{id}")]
	//public async Task<IActionResult> GetByIdAsync(int id)
	//{
	//	var userQueue = await _userQueueRepository.GetById(id);

	//	if (userQueue is null)
	//		return NotFound();

	//	return Ok(userQueue);
	//}

	//[HttpPost("Create")]
	//public async Task<IActionResult> CreateAsync(UserQueueDTO dto)
	//{
	//	var userQueue = _mapper.Map<UserQueue>(dto);

	//	await _userQueueRepository.Add(userQueue);

	//	return Ok(userQueue);
	//}

	//[HttpPut("Update/{id}")]
	//public async Task<IActionResult> Update(int id, [FromBody] UserQueueDTO dto)
	//{
	//	var userQueue = await _userQueueRepository.GetById(id);

	//	if (userQueue is null)
	//		return NotFound();

	//	_mapper.Map(dto, userQueue);
	//	_userQueueRepository.Update(userQueue);

	//	return Ok(userQueue);
	//}

	//[HttpDelete("Delete/{id}")]
	//public async Task<IActionResult> Delete(int id)
	//{
	//	var userQueue = await _userQueueRepository.GetById(id);

	//	if (userQueue is null)
	//		return NotFound();

	//	_userQueueRepository.Delete(userQueue);

	//	return Ok(userQueue);
	//}

}
