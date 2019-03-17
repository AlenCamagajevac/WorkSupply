using System;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Models.WorkLog;

namespace WorkSupply.Core.DTOs.WorkLog
{
    public class WorkLogDto
    {
        public string Id { get; set; }
        
        public ApplicationUserDto Employer { get; set; }
        
        public ApplicationUserDto Employee { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public WorkLogStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime? ResolvedDate { get; set; }
    }
}