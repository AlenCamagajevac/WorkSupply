using System.ComponentModel.DataAnnotations;

namespace WorkSupply.Core.DTOs.Auth
{
    public class RegisterDto
    {
        [MaxLength(50), MinLength(3)]
        public string Email { get; set; }

        [MaxLength(20), MinLength(3)]
        public string Password { get; set; }
    }
}