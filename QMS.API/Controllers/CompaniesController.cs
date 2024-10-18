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
	public class CompaniesController : ControllerBase
	{
		private readonly IBaseRepository<Company> _companyRepository;
		private readonly IMapper _mapper;
		public CompaniesController(IBaseRepository<Company> companyRepository, IMapper mapper)
		{
			_companyRepository = companyRepository;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _companyRepository.GetAll());
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetByIdAsync(long id)
		{
			var company = await _companyRepository.GetById(id);

			if (company is null)
				return NotFound();

			return Ok(company);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateAsync(CompanyDTO dto)
		{
			var company = _mapper.Map<Company>(dto);

			await _companyRepository.Add(company);

			return Ok(company);
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> Update(long id, [FromBody] CompanyDTO dto)
		{
			var company = await _companyRepository.GetById(id);

			if (company is null)
				return NotFound();

			_mapper.Map(dto, company);
			_companyRepository.Update(company);

			return Ok(company);
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var company = await _companyRepository.GetById(id);

			if (company is null)
				return NotFound();

			_companyRepository.Delete(company);

			return Ok(company);
		}

	}
}
