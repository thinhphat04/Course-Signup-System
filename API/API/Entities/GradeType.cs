using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class GradeType 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GradeTypeId { get; set; }

        [StringLength(150)]
        public string GradeTypeName { get; set; } = null!;

        public int Coefficient { get; set; } 

        public ICollection<Grade> Grade { get; set; } = null!;

        public ICollection<SubjectGradeType> SubjectGradeType { get; set; } = new List<SubjectGradeType>();
    }

