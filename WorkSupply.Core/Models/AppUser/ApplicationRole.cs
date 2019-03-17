using System;
using Microsoft.AspNetCore.Identity;

namespace WorkSupply.Core.Models.AppUser
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        
        public static string GetRoleName(Role role)
        {
            return Enum.GetName(typeof(Role), role);
        }
    }
}