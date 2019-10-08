using System.Collections.Generic;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Core.DTOs.Employer
{
    public class EmployerDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public List<ApplicationUserDto> Employees { get; set; }
    }
}