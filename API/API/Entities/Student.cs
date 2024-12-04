using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
    public class Student : User
    {

        [StringLength(50)]
        public string? Parents { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        
        public char Sex { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string? Address { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; } = null!;
    }

