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
	public class IdentityTypesController : ControllerBase
	{
		private readonly IBaseRepository<IdentityType> _identityTypeRepository;
		private readonly IMapper _mapper;
		public IdentityTypesController(IBaseRepository<IdentityType> identityTypeRepository, IMapper mapper)
		{
			_identityTypeRepository = identityTypeRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _identityTypeRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(long id)
		{
			var identityType = await _identityTypeRepository.GetById(id);

			if (identityType is null)
				return NotFound();

			return Ok(identityType);
		}

		[HttpPut("Create")]
		public async Task<IActionResult> CreateAsync(IdentityTypeDTO dto)
		{
			var identityType = _mapper.Map<IdentityType>(dto);

			await _identityTypeRepository.Add(identityType);

			return Ok(identityType);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] IdentityTypeDTO dto)
		{
			var identityType = await _identityTypeRepository.GetById(id);

			if (identityType is null)
				return NotFound();

			_mapper.Map(dto, identityType);

			_identityTypeRepository.Update(identityType);

			return Ok(identityType);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var identityType = await _identityTypeRepository.GetById(id);

			if (identityType is null)
				return NotFound();

			_identityTypeRepository.Delete(identityType);

			return Ok(identityType);
		}

	}
}
