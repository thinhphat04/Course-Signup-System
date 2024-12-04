using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RoleDTO
    {
        public int? RoleId { get; set; } 
        [Required]
        public string RoleName { get; set; } = null!;
    }
}
