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
	public class RolesController : ControllerBase
	{
		private readonly IBaseRepository<Role> _roleRepository;
		private readonly IMapper _mapper;
		public RolesController(IBaseRepository<Role> roleRepository, IMapper mapper)
		{
			_roleRepository = roleRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _roleRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(byte id)
		{
			var role = await _roleRepository.GetById(id);

			if (role is null)
				return NotFound();

			return Ok(role);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(RoleDTO dto)
		{
			var role = _mapper.Map<Role>(dto);

			await _roleRepository.Add(role);

			return Ok(role);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(byte id, [FromBody] RoleDTO dto)
		{
			var role = await _roleRepository.GetById(id);

			if (role is null)
				return NotFound();

			_mapper.Map(dto, role);
			_roleRepository.Update(role);

			return Ok(role);
		}

		[HttpPut("Delete/{id}")]
		public async Task<IActionResult> Delete(byte id)
		{
			var role = await _roleRepository.GetById(id);

			if (role is null)
				return NotFound();

			_roleRepository.Delete(role);

			return Ok(role);
		}

	}
}
