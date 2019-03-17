using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkSupply.Core.Models.WorkLog;

namespace WorkSupply.Core.DTOs.WorkLog
{
    public class ResolveWorkLogDto
    {
        [MaxLength(50), MinLength(3), Required]
        public string WorkLogId { get; set; }

        [Required]
        public WorkLogStatus WorkLogStatus { get; set; }
    }
}