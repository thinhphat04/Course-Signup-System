using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class StudentClassController : ControllerBase
    {
        private readonly IStudentClassService _studentClass;
        public StudentClassController(IStudentClassService studentClass)
        {
            _studentClass = studentClass;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSudentClass([FromBody] StudentClassDTO studentClassDto)
        {
            try
            {
                var studentclass = await _studentClass.CreateStudentClass(studentClassDto);
                return Ok(studentclass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{page}/{pagesize}")]
        public async Task<IActionResult> GetStudentClass([FromQuery] int page =1 , [FromQuery] int pagesize = 10)
        {
            try
            {
                var result = await _studentClass.GetStudentClasses(page, pagesize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentClassById(int Id)
        {
            try
            {
                var result = await _studentClass.GetStudentClassById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudentClass(int Id)
        {
            try
            {
                var result = await _studentClass.DeleteStudentClass(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateStudentClass (int Id,StudentClassDTO studentClassDto)
        {
            try
            {
                var result = await _studentClass.UpdateStudentClass(Id, studentClassDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("student-Satus")]
        public async Task<IActionResult> GetStudentByStatus(bool status)
        {
            try
            {
                var students = await _studentClass.GetStudentByStatus(status);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

