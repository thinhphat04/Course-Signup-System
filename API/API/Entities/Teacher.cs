using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace API.Entities;
    public class Teacher : User
    {
        public DateTime BirthDay { get; set; }

        public char Sex { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string? Address { get; set; }

        public string SubjectId { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        [StringLength(255)]
        public string? PartTimeSubject { get;set; } = null!;

        [StringLength(255)]
        public string TaxCode { get; set; } = null!;

        [StringLength(12)]
        public string IdentityCard { get; set; } = null!;
        public ICollection<TeachSchedule> TeachSchedules { get; set; } = new List<TeachSchedule>();
        public ICollection<EmployeeSalary> EmployeeSalarys { get; set; } = new List<EmployeeSalary>();
    }

