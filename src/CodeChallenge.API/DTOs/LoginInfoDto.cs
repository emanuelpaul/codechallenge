using System;

namespace CodeChallenge.API.DTOs
{
    public class LoginInfoDto
    {
        public string Token { get; set; }

        public DateTime ExpirationDateUtc { get; set; }
    }
}
