using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WorkSupply.Core.Models.AppUser
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        [MaxLength(50), MinLength(3)]
        public string Address { get; set; }

        [MaxLength(350), MinLength(3)]
        public string City { get; set; }
    }
}