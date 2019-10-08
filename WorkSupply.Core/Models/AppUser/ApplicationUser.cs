using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WorkSupply.Core.Models.Employments;

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

        public string EmailConfirmationCode { get; set; }

        [NotMapped]
        public ICollection<Employment> Employments { get; set; }
    }
}