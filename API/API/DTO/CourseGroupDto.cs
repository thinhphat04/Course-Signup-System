using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CourseGroupDto
    {
        [Required]
        public string CourseGroupId { get; set; } = null!;

        [Required]
        public string CourseGroupName { get; set; } = null!;

        public DateTime StartStudy { get; set; }

        public DateTime EndStudy { get; set; }
    }
}
