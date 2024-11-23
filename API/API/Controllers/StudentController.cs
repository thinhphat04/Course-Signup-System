using API.Dto.Student;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // Xem danh sách học viên
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        try
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Thêm học viên
    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
    {
        try
        {
            var student = await _studentService.AddStudentAsync(studentDto);
            return Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Đăng ký ghi danh cho học viên vào lớp
    [HttpPost("{studentId}/enroll")]
    public async Task<IActionResult> EnrollStudent(int studentId, [FromBody] EnrollmentDto enrollmentDto)
    {
        try
        {
            var result = await _studentService.EnrollStudentAsync(studentId, enrollmentDto);
            if (!result) return BadRequest("Enrollment failed");

            return Ok("Enrollment successful");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Xem danh sách lớp học của học viên
    [HttpGet("{studentId}/classes")]
    public async Task<IActionResult> GetStudentClasses(int studentId)
    {
        try
        {
            var classes = await _studentService.GetStudentClassesAsync(studentId);
            return Ok(classes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Xem thời khóa biểu của học viên
    [HttpGet("{studentId}/schedule")]
    public async Task<IActionResult> GetStudentSchedule(int studentId)
    {
        try
        {
            var schedule = await _studentService.GetStudentScheduleAsync(studentId);
            return Ok(schedule);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Cập nhật thông tin học viên
    [HttpPut("{studentId}")]
    public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] StudentDto studentDto)
    {
        try
        {
            var updatedStudent = await _studentService.UpdateStudentAsync(studentId, studentDto);
            if (updatedStudent == null) return NotFound("Student not found");

            return Ok(updatedStudent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // Xóa học viên
    [HttpDelete("{studentId}")]
    public async Task<IActionResult> DeleteStudent(int studentId)
    {
        try
        {
            var result = await _studentService.DeleteStudentAsync(studentId);
            if (!result) return NotFound("Student not found");

            return Ok("Student deleted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
