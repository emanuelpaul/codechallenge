using CodeChallenge.API.DTOs;
using FluentValidation;

namespace CodeChallenge.API.Validation
{
    public class LoginInputDtoValidator : AbstractValidator<LoginInputDto>
    {
        public LoginInputDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
