using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QMS.BL.DTOs;
using QMS.BL.Interfaces;
using QMS.BL.Models;

namespace QMS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceNeededDataController : ControllerBase
	{
		private readonly IBaseRepository<ServiceNeededData> _serviceNeededDataRepository;
		private readonly IMapper _mapper;
		public ServiceNeededDataController(IBaseRepository<ServiceNeededData> serviceNeededDataRepository, IMapper mapper)
		{
			_serviceNeededDataRepository = serviceNeededDataRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _serviceNeededDataRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var neededData = await _serviceNeededDataRepository.GetById(id);

			if (neededData is null)
				return NotFound();

			return Ok(neededData);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(ServiceNeededDataDTO dto)
		{
			var neededData = _mapper.Map<ServiceNeededData>(dto);

			await _serviceNeededDataRepository.Add(neededData);

			return Ok(neededData);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ServiceNeededDataDTO dto)
		{
			var neededData = await _serviceNeededDataRepository.GetById(id);

			if (neededData is null)
				return NotFound();

			_mapper.Map(dto, neededData);

			return Ok(neededData);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var neededData = await _serviceNeededDataRepository.GetById(id);

			if (neededData is null)
				return NotFound();

			_serviceNeededDataRepository.Delete(neededData);

			return Ok(neededData);
		}

	}
}
