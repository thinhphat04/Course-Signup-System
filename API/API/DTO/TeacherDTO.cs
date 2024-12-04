using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class TeacherDTO : UserDTO
    {
        public DateTime BirthDay { get; set; }

        public char Sex { get; set; }

        [Required, StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string? Address { get; set; }

        [Required]
        public string SubjectId { get; set; } = null!;

        [Required, StringLength(255)]
        public string PartTimeSubject { get; set; } = null!;

        [Required,StringLength(255)]
        public string TaxCode { get; set; } = null!;

        [Required,StringLength(12)]
        public string IdentityCard { get; set; } = null!;
    }
}
