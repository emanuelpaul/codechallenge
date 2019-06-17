using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.API.DTOs;
using CodeChallenge.API.Models;
using CodeChallenge.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.API.Controllers
{
    [ApiController]
    [Route("Companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyService companyService,
            IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll() =>
            _mapper.Map<List<CompanyDto>>(await _companyService.GetAllCompaniesAsync());

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Add(CompanyForAddDto companyForAdd)
        {
            Company company = _mapper.Map<Company>(companyForAdd);
            await _companyService.AddAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.CompanyId }, _mapper.Map<CompanyDto>(company));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetById(int id)
        {
            Company company = await _companyService.GetByIdAsync(id);
            if (company != null)
            {
                return _mapper.Map<CompanyDto>(company);
            }

            return NotFound();
        }
    }
}
