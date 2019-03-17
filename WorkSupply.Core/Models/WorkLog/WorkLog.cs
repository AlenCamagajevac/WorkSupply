using System;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Audit;

namespace WorkSupply.Core.Models.WorkLog
{
    public class WorkLog : Entity<string>
    {
        public string EmployerId { get; set; }
        
        public ApplicationUser Employer { get; set; }

        public string EmployeeId { get; set; }
        
        public ApplicationUser Employee { get; set; }

        public TimeSpan Duration { get; set; }

        public WorkLogStatus Status { get; set; }
    }
}