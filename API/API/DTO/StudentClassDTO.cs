using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class StudentClassDTO
    {
        public int? StudentClassId { get; set; }

        public bool Status { get; set; } 

        public string? Name { get; set; } 

        public string? ClassName { get; set; }

        public string UserId { get; set; } = null!;        
        //public StudentDTO Student { get; set; } = null!;

        public string ClassId { get; set; } = null!;       
        //public ClassDTO Class { get; set; } = null!;
    }
}
