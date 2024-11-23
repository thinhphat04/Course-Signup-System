using API.Dto.Teacher;
using API.Dto;

using API.Dto.Student;

namespace API.Interfaces;

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
    Task<TeacherDto> AddTeacherAsync(TeacherDto teacherDto);
    Task<TeacherDto> UpdateTeacherAsync(int teacherId, TeacherDto teacherDto);
    Task<bool> DeleteTeacherAsync(int teacherId);
    Task<IEnumerable<ScheduleDto>> GetTeacherScheduleAsync(int teacherId);
}