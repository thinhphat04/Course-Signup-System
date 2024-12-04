using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class GradeTypeDTO
    {
        public int? GradeTypeId { get; set; }

        [Required]
        public string GradeTypeName { get; set; } = null!;
        [Required]
        public int Coefficient { get; set; } //hệ số
    }
}
