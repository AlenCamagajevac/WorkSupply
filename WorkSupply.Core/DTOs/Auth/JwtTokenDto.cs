using System;

namespace WorkSupply.Core.DTOs.Auth
{
    public class JwtTokenDto
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}