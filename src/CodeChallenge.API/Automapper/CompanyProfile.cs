using AutoMapper;
using CodeChallenge.API.DTOs;
using CodeChallenge.API.Models;

namespace CodeChallenge.API.Automapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();

            CreateMap<CompanyForAddDto, Company>();

            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}
