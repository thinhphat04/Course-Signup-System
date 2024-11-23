using API.Dto.Student;

namespace API.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto> AddStudentAsync(StudentDto studentDto);
    Task<bool> EnrollStudentAsync(int studentId, EnrollmentDto enrollmentDto);
    Task<IEnumerable<ClassDto>> GetStudentClassesAsync(int studentId);
    Task<IEnumerable<ScheduleDto>> GetStudentScheduleAsync(int studentId);
    Task<StudentDto> UpdateStudentAsync(int studentId, StudentDto studentDto);
    Task<bool> DeleteStudentAsync(int studentId);
}