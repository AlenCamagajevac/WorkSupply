using System;

namespace WorkSupply.Core.DTOs.WorkLog
{
    public class WorkLogGraphDataDto
    {
        public string Date { get; set; }

        public TimeSpan TotalHours { get; set; }
        
        public TimeSpan TotalPending { get; set; }

        public TimeSpan TotalApproved { get; set; }

        public TimeSpan TotalRejected { get; set; }
    }
}