using System.ComponentModel.DataAnnotations;

namespace WorkSupply.Core.DTOs.Employment
{
    public class CreateEmploymentDto
    {
        [MaxLength(50), MinLength(3)]
        public string EmployeeId { get; set; }

        [MaxLength(50), MinLength(3)]
        public string EmployerId { get; set; }

        [Required]
        public bool RelationshipShouldExist { get; set; }
    }
}