using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Entities;
    public class SubjectGradeType // diểm môn khôi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Key]
        public int Id { get; set; }

        public int GradeColumn { get; set; } // số cột điểm

        public int MandatoryColumnGrade { get; set; }// số cột điểm bắt buộc

        public string ClassOfId { get; set; } = null!;
        [ForeignKey("ClassOfId")]
        public ClassOf ClassOf { get; set; } = null!;

        public string SubjectId { get; set; } = null!;
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; } = null!;

        public int GradeTypeId { get; set; }
        [ForeignKey("GradeTypeId")]
        public GradeType GradeType { get; set; } = null!;
    }

