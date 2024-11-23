using API.Data;
using API.Dto.Student;
using API.Dto.Teacher;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TeacherService : ITeacherService
{
    private readonly CourseSystemContext _context;
    private readonly IMapper _mapper;

    public TeacherService(CourseSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
    }

    public async Task<TeacherDto> AddTeacherAsync(TeacherDto teacherDto)
    {
        var teacher = _mapper.Map<Teacher>(teacherDto);
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return _mapper.Map<TeacherDto>(teacher);
    }

    public async Task<TeacherDto> UpdateTeacherAsync(int teacherId, TeacherDto teacherDto)
    {
        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher == null) return null;

        _mapper.Map(teacherDto, teacher);
        await _context.SaveChangesAsync();
        return _mapper.Map<TeacherDto>(teacher);
    }

    public async Task<bool> DeleteTeacherAsync(int teacherId)
    {
        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher == null) return false;

        _context.Teachers.Remove(teacher);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ScheduleDto>> GetTeacherScheduleAsync(int teacherId)
    {
        var schedules = await _context.Classes
            .Where(c => c.TeacherId == teacherId)
            .Select(c => new ScheduleDto
            {
                ClassId = c.ClassId,
                CourseName = c.Course.CourseName,
                Schedule = c.Schedule,
                Room = c.Room
            })
            .ToListAsync();

        return schedules;
    }
}
