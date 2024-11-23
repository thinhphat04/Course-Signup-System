using API.Dto.Teacher;
using API.Dto.Teacher;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    // 1. Xem danh sách giảng viên
    [HttpGet]
    public async Task<IActionResult> GetAllTeachers()
    {
        try
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // 2. Thêm giảng viên
    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] TeacherDto teacherDto)
    {
        try
        {
            var teacher = await _teacherService.AddTeacherAsync(teacherDto);
            return Ok(teacher);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // 3. Xem lịch giảng dạy của giảng viên
    [HttpGet("{teacherId}/schedule")]
    public async Task<IActionResult> GetTeacherSchedule(int teacherId)
    {
        try
        {
            var schedule = await _teacherService.GetTeacherScheduleAsync(teacherId);
            return Ok(schedule);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // 4. Cập nhật thông tin giảng viên
    [HttpPut("{teacherId}")]
    public async Task<IActionResult> UpdateTeacher(int teacherId, [FromBody] TeacherDto teacherDto)
    {
        try
        {
            var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacherId, teacherDto);
            if (updatedTeacher == null) return NotFound("Teacher not found");

            return Ok(updatedTeacher);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // 5. Xóa giảng viên
    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> DeleteTeacher(int teacherId)
    {
        try
        {
            var result = await _teacherService.DeleteTeacherAsync(teacherId);
            if (!result) return NotFound("Teacher not found");

            return Ok("Teacher deleted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
