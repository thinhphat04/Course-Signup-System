using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface ISubjectGradeTypeService
    {
        Task<SubjectGradeTypeDTO> CreateSubjectGradeType(SubjectGradeTypeDTO SubjectGradeTypeDTO);
        Task<ServiceResponse> UpdateSubjectGradeType(int Id, SubjectGradeTypeDTO SubjectGradeTypeDTO);
        Task<PageResult<SubjectGradeTypeDTO>> GetSubjectGradeType(int page, int pagesize);
        Task<ServiceResponse> DeleteSubjectGradeTypeType(int Id);
        Task<SubjectGradeTypeDTO> GetSubjectGradeTypeById(int Id);
    }
}
