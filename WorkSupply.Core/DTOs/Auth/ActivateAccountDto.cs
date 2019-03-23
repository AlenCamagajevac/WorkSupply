using System.ComponentModel.DataAnnotations;

namespace WorkSupply.Core.DTOs.Auth
{
    public class ActivateAccountDto
    {
        [MaxLength(150), MinLength(3)]
        public string UserId { get; set; }

        [MaxLength(150), MinLength(3)]
        public string NewPassword { get; set; }

        [MaxLength(150), MinLength(3)]
        public string EmailConfirmationToken { get; set; }
    }
}