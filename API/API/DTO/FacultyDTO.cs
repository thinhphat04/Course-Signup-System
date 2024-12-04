using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class FacultyDTO
    {
        [Required]
        public string FacultyId { get; set; } = null!;
        [Required]
        public string FacultyName { get; set; } = null!;
    }
}
