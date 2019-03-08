using Microsoft.AspNetCore.Identity;

namespace WorkSupply.Core.Models.AppUser
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}