using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class StudentDTO : UserDTO
    {
        public string? Parents { get; set; } = null!;
        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public char Sex { get; set; }

        [Required, StringLength(10)]
        public string PhoneNumber { get; set; } = null!;
       
        public string? Address { get; set; }
        //public ICollection<StudentClassDTO> StudentClassesDTO { get; set; } = null!;

    }
}
