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
	public class FeedbacksController : ControllerBase
	{
		private readonly IBaseRepository<Feedback> _feedbackRepository;
		private readonly IMapper _mapper;
		public FeedbacksController(IBaseRepository<Feedback> feedbackRepository, IMapper mapper)
		{
			_feedbackRepository = feedbackRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _feedbackRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(long id)
		{
			var feedback = await _feedbackRepository.GetById(id);

			if (feedback is null)
				return NotFound();

			return Ok(feedback);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(FeedbackDTO dto)
		{
			var feedbacks = _mapper.Map<Feedback>(dto);

			await _feedbackRepository.Add(feedbacks);

			return Ok(feedbacks);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] FeedbackDTO dto)
		{
			var feedback = await _feedbackRepository.GetById(id);

			if (feedback == null)
				return NotFound();

			_mapper.Map(dto, feedback);
			_feedbackRepository.Update(feedback);

			return Ok(feedback);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var feedback = await _feedbackRepository.GetById(id);

			if (feedback is null)
				return NotFound();

			_feedbackRepository.Delete(feedback);

			return Ok(feedback);
		}

	}
}
