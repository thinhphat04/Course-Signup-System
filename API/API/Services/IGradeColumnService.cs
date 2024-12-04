using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface IGradeColumnService
    {
        Task<GradeColumnDTO> CreateGrade(GradeColumnDTO GradeColumnDTO);
        Task<ServiceResponse> UpdateGrade(int Id, GradeColumnDTO GradeColumnDTO);
        Task<List<GradeColumnDTO>> GetGrade();
        Task<ServiceResponse> DeleteGradeType(int GradeColumnId);
        Task<GradeColumnDTO> GetGradeById(int GradeColumnId);
    }
}
