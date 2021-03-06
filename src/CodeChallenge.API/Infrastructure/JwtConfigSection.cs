﻿namespace CodeChallenge.API.Infrastructure
{
    public class JwtConfigSection
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Key { get; set; }

        public int ExpiresAfterInMinutes { get; set; }
    }
}
