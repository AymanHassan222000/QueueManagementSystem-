using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
	//private readonly IBaseRepository<Service> _serviceRepository;
	//private readonly IMapper _mapper;
	//public ServicesController(IBaseRepository<Service> serviceRepository, IMapper mapper)
	//{
	//	_serviceRepository = serviceRepository;
	//	_mapper = mapper;
	//}

	//[HttpGet("GetAll")]
	//public async Task<IActionResult> GetAllAsync()
	//{
	//	return Ok(await _serviceRepository.GetAll());
	//}

	//[HttpGet("GetById/{id}")]
	//public async Task<IActionResult> GetByIdAsync(byte id)
	//{
	//	var service = await _serviceRepository.GetById(id);

	//	if (service is null)
	//		return NotFound();

	//	return Ok(service);
	//}

	//[HttpPost("Create")]
	//public async Task<IActionResult> CreateAsync(ServiceDTO dto)
	//{
	//	var service = _mapper.Map<Service>(dto);

	//	await _serviceRepository.Add(service);

	//	return Ok(service);
	//}

	//[HttpPut("Update/{id}")]
	//public async Task<IActionResult> Update(byte id, [FromBody] ServiceDTO dto)
	//{
	//	var service = await _serviceRepository.GetById(id);

	//	if (service is null)
	//		return NotFound();

	//	_mapper.Map(dto, service);
	//	_serviceRepository.Update(service);

	//	return Ok(service);
	//}

	//[HttpPut("Delete/{id}")]
	//public async Task<IActionResult> Delete(byte id)
	//{
	//	var service = await _serviceRepository.GetById(id);

	//	if (service is null)
	//		return NotFound();

	//	_serviceRepository.Delete(service);

	//	return Ok(service);
	//}

}
