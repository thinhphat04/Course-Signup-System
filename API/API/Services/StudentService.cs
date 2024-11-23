using API.Data;
using API.Dto.Student;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class StudentService : IStudentService
{
    private readonly CourseSystemContext _context;
    private readonly IMapper _mapper;

    public StudentService(CourseSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _context.Students.ToListAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto> AddStudentAsync(StudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<bool> EnrollStudentAsync(int studentId, EnrollmentDto enrollmentDto)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
        enrollment.StudentId = studentId;

        _context.Enrollments.Add(enrollment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ClassDto>> GetStudentClassesAsync(int studentId)
    {
        var classes = await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Select(e => e.Class)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ClassDto>>(classes);
    }

    public async Task<IEnumerable<ScheduleDto>> GetStudentScheduleAsync(int studentId)
    {
        var schedules = await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Select(e => new ScheduleDto
            {
                ClassId = e.ClassId,
                Schedule = e.Class.Schedule,
                Room = e.Class.Room,
                CourseName = e.Class.Course.CourseName
            })
            .ToListAsync();

        return schedules;
    }

    public async Task<StudentDto> UpdateStudentAsync(int studentId, StudentDto studentDto)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return null;

        _mapper.Map(studentDto, student);
        await _context.SaveChangesAsync();
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<bool> DeleteStudentAsync(int studentId)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        _context.Students.Remove(student);
        return await _context.SaveChangesAsync() > 0;
    }
}
