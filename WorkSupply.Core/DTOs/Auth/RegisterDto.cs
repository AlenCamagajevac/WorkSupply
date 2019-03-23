using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Core.DTOs.Auth
{
    public class RegisterDto
    {
        [MaxLength(150), MinLength(3)]
        public string Email { get; set; }
        
        [MaxLength(150), MinLength(3)]
        public string Name { get; set; }
        
        [MaxLength(150), MinLength(3)]
        public string City { get; set; }
        
        [MaxLength(150), MinLength(3)]
        public string Address { get; set; }
        
        [Required]
        public Role Role { get; set; }
    }
}