using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class GradeDTO
    {
        public int? GradeId { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string SubjectId { get; set; } = null!;

        public double? AverageScore { get; set; }
        [Required]
        public int GradeTypeId { get; set; }
        public ICollection<GradeColumnDTO> GradeColumns { get; set; } = new List<GradeColumnDTO>();


    }
}
