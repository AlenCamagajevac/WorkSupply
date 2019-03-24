using System;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Audit;

namespace WorkSupply.Core.Models.Employments
{
    public class Employment : Entity<string>
    {
        public string EmployerId { get; set; }
        
        public ApplicationUser Employer { get; set; }

        public string EmployeeId { get; set; }
        
        public ApplicationUser Employee { get; set; }

        public bool IsActive { get; set; }
    }
}