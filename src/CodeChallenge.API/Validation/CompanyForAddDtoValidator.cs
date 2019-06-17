using CodeChallenge.API.DTOs;
using CodeChallenge.API.Services.Abstract;
using FluentValidation;

namespace CodeChallenge.API.Validation
{
    public class CompanyForAddDtoValidator : AbstractValidator<CompanyForAddDto>
    {
        public CompanyForAddDtoValidator(ICompanyService companyService)
        {
            RuleFor(x => x.Exchange).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Isin).NotEmpty().Length(12).Matches("^[A-Za-z]{2}[a-zA-Z0-9]*$");//Must match https://en.wikipedia.org/wiki/International_Securities_Identification_Number#Description
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Ticker).NotEmpty().MaximumLength(5);
            RuleFor(x => x.Website).MaximumLength(120);
            RuleFor(x => x.Isin).MustAsync(async (isin, cancellation) => await companyService.CanIsinByUsed(isin, null)).WithMessage("This ISIN is already used!");
        }
    }
}
