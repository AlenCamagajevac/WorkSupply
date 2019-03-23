using System;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.Core.Models.WorkLog;

namespace WorkSupply.Core.Query
{
    public class WorkLogGraphDataQuery
    {
        [FromQuery]
        public DateTime? StartDate { get; set; }

        [FromQuery]
        public DateTime? EndDate { get; set; }

        [FromQuery] 
        public TimeGranularity Granularity { get; set; }
    }
}