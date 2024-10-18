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
	public class QueuesController : ControllerBase
	{
		private readonly IBaseRepository<Queue> _queueRepository;
		private readonly IMapper _mapper;
		public QueuesController(IBaseRepository<Queue> queueRepository, IMapper mapper)
		{
			_queueRepository = queueRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _queueRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(long id)
		{
			var queue = await _queueRepository.GetById(id);

			if (queue is null)
				return NotFound();

			return Ok(queue);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(QueueDTO dto)
		{
			var queue = _mapper.Map<Queue>(dto);

			await _queueRepository.Add(queue);

			return Ok(queue);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] QueueDTO dto)
		{
			var queue = await _queueRepository.GetById(id);

			if (queue is null)
				return NotFound();

			_mapper.Map(dto, queue);
			_queueRepository.Update(queue);

			return Ok(queue);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var queue = await _queueRepository.GetById(id);

			if (queue is null)
				return NotFound();

			_queueRepository.Delete(queue);

			return Ok(queue);
		}

	}
}
