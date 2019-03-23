using System;

namespace WorkSupply.Core.Models.WorkLog
{
    public class WorkLogGraphData
    {
        public string Date { get; set; }

        public long TotalApproved { get; set; }

        public long TotalRejected { get; set; }

        public long TotalPending { get; set; }

        public long TotalHours { get; set; }
    }
}