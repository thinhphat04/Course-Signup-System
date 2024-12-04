using API.DTO;
using API.DTO.Reponse;

namespace API.Services
{
    public interface IStudentClassService
    {
        Task<StudentClassDTO> CreateStudentClass(StudentClassDTO studentClassDTO);
        Task<ServiceResponse> UpdateStudentClass(int Id,StudentClassDTO studentClassDTO);
        Task<PageResult<StudentClassDTO>> GetStudentClasses(int page, int pagesize);
        Task<ServiceResponse> DeleteStudentClass(int StudentClassId);
        Task<StudentClassDTO> GetStudentClassById(int StudentClassId);
        Task<List<StudentClassDTO>> GetStudentByStatus(bool status);
        Task<ServiceResponse> CheckTuitionFeePayment(string StudentId, string ClassId);
        
    }
}
