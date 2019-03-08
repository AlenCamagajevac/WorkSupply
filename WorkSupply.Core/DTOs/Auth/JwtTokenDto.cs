namespace WorkSupply.Core.DTOs.Auth
{
    public class JwtToken
    {
        public string Token { get; set; }

        public string Expiration { get; set; }
    }
}