using API.Entities;
using System.Runtime.CompilerServices;

namespace API.DTO.Reponse
{
    public class AcademicTranscript
    {
        public string StudentName { get; set; } = null!;

        public string SubjectName { get; set; } = null!;
        public List<GradeColumnDTO> GradeColumns { get; set; } = null!;
        public List<GradeTypeDTO> GradeTypes { get; set; } = null!;
        public double SubjectGradePointAverage {  get; set; }
    }
}
