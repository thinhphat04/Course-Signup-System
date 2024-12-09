using API.DTO;
using API.DTO.Reponse;
using API.Entities;

namespace API.Services
{
    public interface IStudentService
    {
        Task<StudentDTO> GetStudentById(string id);
        Task<StudentDTO> CreateStudent(StudentDTO student);
        Task<ServiceResponse> UpdateStudent(StudentDTO student);
        Task<ServiceResponse> DeleteStudent(string Id);
        Task<PageResult<StudentDTO>> GetAllStudents(int page, int pagesize);
        Task<List<StudentDTO>> GetStudentByEmail(string Email);
        Task<List<TeachSchedule>> GetScheduleClass(string StudentId);

    }
}
