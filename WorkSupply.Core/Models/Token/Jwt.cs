using System;

namespace WorkSupply.Core.Models.Token
{
    public class Jwt
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}