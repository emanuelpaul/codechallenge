using CodeChallenge.API.DTOs;
using FluentValidation;

namespace CodeChallenge.API.Validation
{
    public class CompanyForUpdateDtoValidator : AbstractValidator<CompanyForUpdateDto>
    {
        public CompanyForUpdateDtoValidator()
        {
            RuleFor(x => x.Exchange).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Isin).NotEmpty().Length(12).Matches("^[A-Za-z]{2}[a-zA-Z0-9]*$");//Must match https://en.wikipedia.org/wiki/International_Securities_Identification_Number#Description
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Ticker).NotEmpty().MaximumLength(5);
            RuleFor(x => x.Website).MaximumLength(120);
        }
    }
}
