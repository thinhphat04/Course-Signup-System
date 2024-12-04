using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class SchoolHolidaySchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string NameHoliday { set; get; } = null!;

        [StringLength(250)]
        public string Reason { set; get; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }   
    }

