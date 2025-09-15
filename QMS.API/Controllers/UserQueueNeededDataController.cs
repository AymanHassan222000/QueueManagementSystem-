using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserQueueNeededDataController : ControllerBase
{
	//private readonly IBaseRepository<UserQueueNeededData> _userQueueNeededDataRepository;
	//private readonly IMapper _mapper;
	//public UserQueueNeededDataController(IBaseRepository<UserQueueNeededData> userQueueNeededDataRepository, IMapper mapper)
	//{
	//	_userQueueNeededDataRepository = userQueueNeededDataRepository;
	//	_mapper = mapper;
	//}

	//[HttpGet("GetAll")]
	//public async Task<IActionResult> GetAllAsync()
	//{
	//	return Ok(await _userQueueNeededDataRepository.GetAll());
	//}

	//[HttpGet("GetById/{id}")]
	//public async Task<IActionResult> GetByIdAsync(long id)
	//{
	//	var Data = await _userQueueNeededDataRepository.GetById(id);

	//	if (Data is null)
	//		return NotFound();

	//	return Ok(Data);
	//}

	//[HttpPost("Create")]
	//public async Task<IActionResult> CreateAsync(UserQueueNeededDataDTO dto)
	//{
	//	var Data = _mapper.Map<UserQueueNeededData>(dto);

	//	await _userQueueNeededDataRepository.Add(Data);

	//	return Ok(Data);
	//}

	//[HttpPut("Update/{id}")]
	//public async Task<IActionResult> Update(long id, [FromBody] UserQueueNeededDataDTO dto)
	//{
	//	var Data = await _userQueueNeededDataRepository.GetById(id);

	//	if (Data is null)
	//		return NotFound();

	//	_mapper.Map(dto, Data);
	//	_userQueueNeededDataRepository.Update(Data);

	//	return Ok(Data);
	//}

	//[HttpDelete("Delete/{id}")]
	//public async Task<IActionResult> Delete(long id)
	//{
	//	var Data = await _userQueueNeededDataRepository.GetById(id);

	//	if (Data is null)
	//		return NotFound();

	//	_userQueueNeededDataRepository.Delete(Data);

	//	return Ok(Data);
	//}

}
