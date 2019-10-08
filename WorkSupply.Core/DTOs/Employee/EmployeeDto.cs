using System.Collections.Generic;
using WorkSupply.Core.DTOs.Auth;

namespace WorkSupply.Core.DTOs.Employee
{
    public class EmployeeDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        
        public List<ApplicationUserDto> Employers { get; set; }
    }
}