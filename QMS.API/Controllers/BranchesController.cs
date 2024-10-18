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
	public class BranchesController : ControllerBase
	{
		private readonly IBaseRepository<Branch> _branchRepository;
		private readonly IMapper _mapper;
		public BranchesController(IBaseRepository<Branch> branchRepository, IMapper mapper)
		{
			_branchRepository = branchRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _branchRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(long id)
		{
			var branch = await _branchRepository.GetById(id);

			if (branch is null)
				return NotFound();

			return Ok(branch);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(BranchDTO dto)
		{
			var branch = _mapper.Map<Branch>(dto);

			await _branchRepository.Add(branch);

			return Ok(branch);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] BranchDTO dto)
		{
			var branch = await _branchRepository.GetById(id);

			if (branch is null)
				return NotFound();

			_mapper.Map(dto, branch);
			_branchRepository.Update(branch);

			return Ok(branch);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var branch = await _branchRepository.GetById(id);

			if (branch is null)
				return NotFound();

			_branchRepository.Delete(branch);

			return Ok(branch);
		}

	}
}
