using System;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.Core.Models.WorkLog;

namespace WorkSupply.Core.Query
{
    public class WorkLogQuery
    {
        [FromQuery(Name = "FilterId")]
        public string FilterId { get; set; }

        [FromQuery]
        public DateTime? StartDate { get; set; }

        [FromQuery]
        public DateTime? EndDate { get; set; }

        [FromQuery]
        public WorkLogStatus? Status { get; set; }
        
        [FromQuery] 
        public int? Page { get; set; }
    }
}