using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface ISubjectService
    {
        Task<SubjectDTO> CreateSubject(SubjectDTO SubjectDTO);
        Task<PageResult<SubjectDTO>> GetAllSubject(int page,int pagesize);
        Task<ServiceResponse> DeleteSubject(string SubjectId);
        Task<SubjectDTO> GetSubjectById(string SubjectId);
        Task<ServiceResponse> UpdateSubject(SubjectDTO SubjectDTO);
        Task<List<SubjectDTO>> SearchSubjectByName(string SubjectName);
        Task<List<SubjectDTO>> SearchSubject(int DepartmentId,string FacultyId);
        
    }
}
