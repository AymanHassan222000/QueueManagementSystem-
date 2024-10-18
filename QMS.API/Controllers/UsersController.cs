using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QMS.BL.DTOs;
using QMS.BL.Interfaces;
using QMS.BL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QMS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IBaseRepository<User> _userRepository;
		private readonly JWT _jwtOptions;
		private readonly IMapper _mapper;
		public UsersController(IBaseRepository<User> userRepository, JWT jwtOptions, IMapper mapper)
		{
			_userRepository = userRepository;
			_jwtOptions = jwtOptions;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _userRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(long id)
		{
			var user = await _userRepository.GetById(id);

			if (user is null)
				return NotFound();

			return Ok(user);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(UserDTO dto)
		{
			var user = _mapper.Map<User>(dto);

			await _userRepository.Add(user);

			return Ok(user);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] UserDTO dto)
		{
			var user = await _userRepository.GetById(id);

			if (user is null)
				return NotFound();

			_mapper.Map(dto, user);
			_userRepository.Update(user);

			return Ok(user);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var user = await _userRepository.GetById(id);

			if (user is null)
				return NotFound();

			_userRepository.Delete(user);

			return Ok(user);
		}
	}
}
