using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;
    public class Department// tổ bộ môn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DepartmentId { get; set; }

        [StringLength(150)]
        public string DepartmentName { get; set; } = null!;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public ICollection<SubjectClass> SubjectClasses { get; set; } = new List<SubjectClass>();
    }
