using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class Subject
    {
        [Key, StringLength(20)]
        public string SubjectId { get; set; } = null!;

        [StringLength(100)]
        public string SubjectName { get; set; } = null!;

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } = null!;

        public string FacultyId {  get; set; } = null!;
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; } = null!;

        public ICollection<SubjectClass> SubjectClasses { get; set; } = new List<SubjectClass>();
        public ICollection<SubjectGradeType> SubjectGradeTypes { get; set;} = new List<SubjectGradeType>();
        public ICollection<TeachSchedule> TeachSchedules { get; set; } = new List<TeachSchedule>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    }

