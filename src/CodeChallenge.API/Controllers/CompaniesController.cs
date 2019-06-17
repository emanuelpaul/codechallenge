using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.API.Controllers
{
    [ApiController]
    [Route("Companies")]
    public class CompaniesController : ControllerBase
    {
        public ActionResult<IEnumerable<CompanyDto>> GetAll() =>
            new[] { new CompanyDto { Name = "UiPath" } };
    }
}
