using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class SubjectDTO
    {
        [Required]
        public string SubjectId { get; set; } = null!;
        [Required]
        public string SubjectName { get; set; } = null!;
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string FacultyId { get; set; } = null!;
    }
}
