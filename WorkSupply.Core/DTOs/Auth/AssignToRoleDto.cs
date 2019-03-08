using System.ComponentModel.DataAnnotations;

namespace WorkSupply.Core.DTOs.Auth
{
    public class AssignToRoleDto
    {
        [MaxLength(100)]
        public string UserId { get; set; }

        [MaxLength(100)]
        public string RoleId { get; set; }
    }
}