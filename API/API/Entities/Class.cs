using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

    public class Class
    {
        [Key, StringLength(20)]
        public string ClassId { get; set; } = null!;

        [StringLength(100)]
        public string ClassName { get; set; } = null!;

        public bool Status { get; set; }

        public double Tuition { get; set; } 

        public int NumberStudent { get; set; }

        public int MaxNumberStudent { get; set; }

        [StringLength(255)]
        public string? Description { get; set; } = null!;

        [StringLength(255)]
        public string? Avatar {  get; set; }=null!;

        public string CourseGroupId { get; set; } = null!;
        [ForeignKey("CourseGroupId")]
        public CourseGroup CourseGroup { get; set; } = null!;

        public string FacultyId { get; set; } = null!;
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; } = null!;

        public ICollection<StudentClass> StudentClasses {  get; set; } = null!;
        public ICollection<TeachSchedule> TeachSchedules { set; get; } = null!;                                                                 
        public ICollection<SubjectClass> SubjectClasses { get; set; } = null!;

    }

