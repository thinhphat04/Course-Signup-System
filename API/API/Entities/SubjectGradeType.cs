using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Entities;
    public class SubjectGradeType 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Key]
        public int Id { get; set; }

        public int GradeColumn { get; set; }

        public int MandatoryColumnGrade { get; set; }

        public string CourseGroupId { get; set; } = null!;
        [ForeignKey("CourseGroupId")]
        public CourseGroup CourseGroup { get; set; } = null!;

        public string SubjectId { get; set; } = null!;
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; } = null!;

        public int GradeTypeId { get; set; }
        [ForeignKey("GradeTypeId")]
        public GradeType GradeType { get; set; } = null!;
    }

