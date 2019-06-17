using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.API.Controllers;
using CodeChallenge.API.DTOs;
using CodeChallenge.API.Models;
using CodeChallenge.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CodeChallenge.API.UnitTests.ControllerTests
{
    public class CompaniesControllerTests : IClassFixture<ControllerTestFixture>
    {
        private CompaniesController _companiesController;
        private readonly Mock<ICompanyService> _companyServiceMock = new Mock<ICompanyService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public CompaniesControllerTests()
        {
            _companiesController = new CompaniesController(_companyServiceMock.Object, Mapper.Instance);
        }

        [Fact]
        public async Task GetAll_Returns_ActionResult()
        {
            ActionResult<IEnumerable<CompanyDto>> result = await _companiesController.GetAll();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAll_Calls_GetAllCompaniesAsync()
        {
            Company[] companies = new[] { new Company { Name = "Name", CompanyId = 1 }, new Company { Name = "Name2", CompanyId = 2 } };
            _companyServiceMock.Setup(x => x.GetAllCompaniesAsync()).ReturnsAsync(companies);
            ActionResult<IEnumerable<CompanyDto>> result = await _companiesController.GetAll();
            _companyServiceMock.Verify(x => x.GetAllCompaniesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAll_Returns_SameNumberOfElementsAsCompanyService()
        {
            Company[] companies = new[] { new Company { Name = "Name", CompanyId = 1 }, new Company { Name = "Name2", CompanyId = 2 } };
            _companyServiceMock.Setup(x => x.GetAllCompaniesAsync()).ReturnsAsync(companies);
            ActionResult<IEnumerable<CompanyDto>> result = await _companiesController.GetAll();
            Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public async Task Add_Returns_ActionResult()
        {
            CompanyForAddDto companyDto = new CompanyForAddDto();
            ActionResult<CompanyDto> result = await _companiesController.Add(companyDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Add_Returns_CreatedAtAction()
        {
            CompanyForAddDto companyDto = new CompanyForAddDto();
            ActionResult<CompanyDto> result = await _companiesController.Add(companyDto);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Add_Returns_ActionNameGetById()
        {
            CompanyForAddDto companyDto = new CompanyForAddDto();
            ActionResult<CompanyDto> result = await _companiesController.Add(companyDto);
            CreatedAtActionResult actionResult = result.Result as CreatedAtActionResult;
            Assert.Equal(nameof(CompaniesController.GetById), actionResult.ActionName);
        }

        [Fact]
        public async Task Add_Returns_RouteIdEqualToCompanyId()
        {
            CompanyForAddDto companyDto = new CompanyForAddDto();
            Company company = new Company { CompanyId = 5 };
            _companiesController = new CompaniesController(_companyServiceMock.Object, _mapperMock.Object);
            _mapperMock.Setup(x => x.Map<Company>(companyDto)).Returns(company);
            ActionResult<CompanyDto> result = await _companiesController.Add(companyDto);
            CreatedAtActionResult actionResult = result.Result as CreatedAtActionResult;
            Assert.Equal(5, actionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Add_Returns_CompanyDtoInCreatedAtRoute()
        {
            CompanyForAddDto companyForAddDto = new CompanyForAddDto();
            CompanyDto companyDto = new CompanyDto { CompanyId = 5 };
            Company company = new Company { CompanyId = 5 };
            _companiesController = new CompaniesController(_companyServiceMock.Object, _mapperMock.Object);
            _mapperMock.Setup(x => x.Map<Company>(companyForAddDto)).Returns(company);
            _mapperMock.Setup(x => x.Map<CompanyDto>(company)).Returns(companyDto);
            ActionResult<CompanyDto> result = await _companiesController.Add(companyForAddDto);
            CreatedAtActionResult actionResult = result.Result as CreatedAtActionResult;
            Assert.Equal(companyDto, actionResult.Value);
        }

        [Fact]
        public async Task Add_Calls_CompanyServiceAddAsync()
        {
            CompanyForAddDto companyForAddDto = new CompanyForAddDto();
            Company company = new Company { CompanyId = 5 };
            _companiesController = new CompaniesController(_companyServiceMock.Object, _mapperMock.Object);
            _mapperMock.Setup(x => x.Map<Company>(companyForAddDto)).Returns(company);
            await _companiesController.Add(companyForAddDto);

            _companyServiceMock.Verify(x => x.AddAsync(company), Times.Once());
        }
    }
}
