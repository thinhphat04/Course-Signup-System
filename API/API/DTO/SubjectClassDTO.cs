using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class SubjectClassDTO
    {
        public int Id { get; set; }

        public bool IsClose { get; set; } = false;
        public string? SubjectName { get; set; } 
        public string SubjectId { get; set; } = null!;
        public string? ClassName { get; set; } 
        public string ClassId { get; set; } = null!;
        public string? DepartmentName { get; set; }
        public int DepartmentId { get; set; }

    }
}
