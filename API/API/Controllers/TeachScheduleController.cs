using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TeachScheduleController : ControllerBase
    {
        private readonly ITeacherScheduleService _service;
        public TeachScheduleController(ITeacherScheduleService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeachSchedule([FromBody] TeacherScheduleDTO teacherScheduleDTO)
        {
            try
            {
                var teachSchedule = await _service.CreateTeacherSchedule(teacherScheduleDTO);
                return Ok(teachSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachSchedule([FromQuery] int page = 1, [FromQuery] int pagesize = 10)
        {
            try
            {
                var teachSchedule = await _service.GetTeacherSchedule(page, pagesize);
                return Ok(teachSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTeachScheduleById(int Id)
        {
            try
            {
                var teachSchedule = await _service.GetTeacherScheduleById(Id);
                return Ok(teachSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTeachSchedule(int Id)
        {
            try
            {
                var teachSchedule = await _service.DeleteTeacherSchedule(Id);
                return Ok(teachSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTeachSchedule(int Id,TeacherScheduleDTO teacherScheduleDTO)
        {
            try
            {
                var teachSchedule = await _service.UpdateTeacherSchedule(Id, teacherScheduleDTO);
                return Ok(teachSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

