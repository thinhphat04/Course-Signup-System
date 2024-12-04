using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class TeachSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public TimeSpan StudyTime { get; set; }

        public TimeSpan StudyTimeEnd { get; set; }

       // [StringLength(50)]
        public string ClassRoom { get; set; } = null!;

        public DayOfWeek StudyDay { get; set; } 

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public Teacher Teacher { get; set; } = null!;

        public string ClassId { get; set; } = null!;
        [ForeignKey("ClassId")]
        public Class Class { get; set; } = null!;

      
        public string SubjectId { get; set; } = null!;
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; } = null!;

    }

