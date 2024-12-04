using API.DTO;
using API.DTO.Reponse;

namespace API.Services
{
    public interface ITeacherService
    {
        Task<TeacherDTO> GetTeacherById(string id);
        Task<TeacherDTO> CreateTeacher(TeacherDTO teacher);
        Task<ServiceResponse> UpdateTeacher(TeacherDTO teacher);
        Task<ServiceResponse> DeleteTeacher(string Id);
        Task<PageResult<TeacherDTO>> GetAllTeachers(int page, int pagesize);
        Task<List<TeacherDTO>> GetTeacherByEmail(string Email);
        Task<List<TeacherDTO>> SearchTeacher(string Name);
        Task<TeacherSalary> GetSalaryOfTeacher( string TeacherId);
    }
}
