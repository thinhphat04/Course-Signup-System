using AutoMapper;
using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] int page = 1, [FromQuery] int pagesize =10)
        {
            try
            {
                var students = await _studentService.GetAllStudents(page, pagesize);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudents(string Id)
        {
            try
            {
                var student = await _studentService.GetStudentById(Id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDTO studentDTO)
        {
            try
            {
                var st = await _studentService.CreateStudent(studentDTO);
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(string Id)
        {
            try
            {
                var student = await _studentService.DeleteStudent(Id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateStudent(string Id,StudentDTO studentDTO)
        {
            if(Id != studentDTO.UserId)
            {
                return BadRequest();
            }
            try
            {         
                var student = await _studentService.UpdateStudent(studentDTO);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Get-Student")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            try
            {
                var Student = await _studentService.GetStudentByEmail(email);
                return Ok(Student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}/schedules")]
        public async Task<IActionResult> GetScheduleByStudent(string Id)
        {
            try
            {
                var schedule = await _studentService.GetScheduleClass(Id);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

