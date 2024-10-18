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
	public class SubscriptionsController : ControllerBase
	{
		private readonly IBaseRepository<Subscription> _subscriptionRepository;
		private readonly IMapper _mapper;

		public SubscriptionsController(IBaseRepository<Subscription> subscriptionRepository, IMapper mapper)
		{
			_subscriptionRepository = subscriptionRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _subscriptionRepository.GetAll());
		}

		[HttpGet("GetById/id")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var subscription = await _subscriptionRepository.GetById(id);

			if (subscription is null)
				return NotFound();

			return Ok(subscription);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync([FromBody] SubscriptionDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var subscription = _mapper.Map<Subscription>(dto);

			await _subscriptionRepository.Add(subscription);

			return Ok(subscription);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] SubscriptionDTO dto)
		{
			var subscription = await _subscriptionRepository.GetById(id);

			if (subscription is null)
				return NotFound();

			_mapper.Map(dto, subscription);
			_subscriptionRepository.Update(subscription);

			return Ok(subscription);
		}

		[HttpPut("Delete/{id}")]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var subscription = await _subscriptionRepository.GetById(id);

			if (subscription is null)
				return NotFound();

			_subscriptionRepository.Delete(subscription);

			return Ok(subscription);
		}

	}
}
