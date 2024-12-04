using AutoMapper;
using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers([FromQuery] int page = 1 ,int pagesize = 10)
        //page: Số trang bạn muốn lấy (bắt đầu từ 1).
        //pageSize: Số lượng bản ghi trên mỗi trang.
        {
            try
            {
                var teachers = await _teacherService.GetAllTeachers(page, pagesize);
               
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTeacher(string Id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(Id);
                
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody]TeacherDTO teacherDTO)
        {
            try
            {
                var st = await _teacherService.CreateTeacher(teacherDTO);              
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTeacher(string Id)
        {
            try
            {
                var teacher = await _teacherService.DeleteTeacher(Id);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTeacher(string Id,TeacherDTO teacherDTO)
        {
            if (teacherDTO.UserId != Id)
            {
                return BadRequest("id and teacherid don't ");
            }
            try
            {
                
                var teacher = await _teacherService.UpdateTeacher(teacherDTO);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-teacher")]
        public async Task<IActionResult> GetTeacherByEmail(string Email)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByEmail(Email);
                return Ok(teacher);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("search-teacher/{name}")]
        public async Task<IActionResult> Searchteachers(string name)
        {
            var teacher = await _teacherService.SearchTeacher(name);
            return Ok(teacher);
        }

        [HttpGet("GetSalaryTeacher")]
        public async Task<IActionResult> GetSalaryOfTeacher(string Id)
        {
            try
            {
                var teacher = await _teacherService.GetSalaryOfTeacher(Id);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }


