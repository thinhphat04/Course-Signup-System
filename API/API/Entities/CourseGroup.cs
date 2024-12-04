using System.ComponentModel.DataAnnotations;

namespace API.Entities;

    public class CourseGroup
    {
        [Key, StringLength(20)]
        public string ClassOfId { get; set; } = null!;

        [StringLength(150)]
        public string ClassOfName { get; set; } = null!;

        public DateTime StartStudy { get; set; }

        public DateTime EndStudy { get; set; }

        public ICollection<SubjectGradeType> SubjectGradeTypes { get; set; } = null!;
    }

