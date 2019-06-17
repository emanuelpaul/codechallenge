using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeChallenge.API.DTOs;
using CodeChallenge.API.Infrastructure;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CodeChallenge.API.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly JwtConfigSection _jwtConfig;

        //hardcoded accounts are used just for simplicity. On a real system Asp.net identity Core is one of the options that can be used
        private readonly LoginInputDto[] _accounts = new[]
        {
            new LoginInputDto{ Username = "heisenberg", Password = "IAmTheOneWhoKnocks"},
            new LoginInputDto{ Username = "tyrion", Password="IDrinkAndIKnowThings"}
        };

        public AccountController(JwtConfigSection jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<LoginInfoDto> Login(LoginInputDto loginInput)
        {
            LoginInputDto user = _accounts.FirstOrDefault(x => x.Username.Equals(loginInput.Username.Trim(), StringComparison.OrdinalIgnoreCase) && x.Password == loginInput.Password.Trim());
            if (user != null)
            {
                DateTime expiresUtc = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiresAfterInMinutes);
                return new LoginInfoDto
                {
                    ExpirationDateUtc = expiresUtc,
                    Token = GetJwt(user, expiresUtc)
                };
            }

            return Unauthorized();
        }

        private string GetJwt(LoginInputDto userModel, DateTime expireDateUtc)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, userModel.Username),
                new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                              issuer: _jwtConfig.Issuer,
                              audience: _jwtConfig.Audience,
                              claims: claims,
                              expires: expireDateUtc,
                              signingCredentials: new SigningCredentials(
                                             new SymmetricSecurityKey(Convert.FromBase64String(_jwtConfig.Key)),
                                             SecurityAlgorithms.HmacSha256));

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
