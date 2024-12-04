using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class SubjectGradeTypeDTO
    {
        public int? Id { get; set; }

        public int GradeColumn { get; set; } // số cột điểm

        public int MandatoryColumnGrade { get; set; }// số cột điểm bắt buộc

        public string CourseGroupId { get; set; } = null!;
        public string? CourseGroupName { get; set; } = null!;

        public string SubjectId { get; set; } = null!;
        public string? SubjectName { get; set; } = null!;

        public int GradeTypeId { get; set; }
        public string? GradeTypeName { get; set; } = null!;

    }
}
