using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkSupply.Core.DTOs.WorkLog
{
    public class CreateWorkLogDto
    {
        [MaxLength(50), MinLength(3), Required]
        public string EmployeeId { get; set; }

        [MaxLength(50), MinLength(3), Required]
        public string EmployerId { get; set; }

        [Required]
        public TimeSpan? Duration { get; set; }
    }
}