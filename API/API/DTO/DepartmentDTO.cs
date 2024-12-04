using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class DepartmentDTO
    {
        public int? DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; } = null!;
    }
}
