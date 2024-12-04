using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolHolidayScheduleController : ControllerBase
    {
        private readonly ISchoolHolidayScheduleService _schoolHolidayScheduleService;
        public SchoolHolidayScheduleController(ISchoolHolidayScheduleService schoolHolidayScheduleService)
        {
            _schoolHolidayScheduleService = schoolHolidayScheduleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSchoolHolidaySchedule(SchoolHolidayScheduleDTO schoolHolidayScheduleDTO)
        {
            try
            {
                var sc = await _schoolHolidayScheduleService.CreateSchoolHolidaySchedule(schoolHolidayScheduleDTO);
                return Ok(sc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> CreateSchoolHolidaySchedule(int Id)
        {
            try
            {
                var sc = await _schoolHolidayScheduleService.DeleteSchoolHolidaySchedule(Id);
                return Ok(sc);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateSchoolHolidaySchedule()
        {
            try
            {
                var sc = await _schoolHolidayScheduleService.GetAllSchoolHolidaySchedules();
                return Ok(sc);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSchoolHolidayScheduleById(int Id)
        {
            try
            {
                var sc = await _schoolHolidayScheduleService.GetSchoolHolidaySchedule(Id);
                return Ok(sc);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> CreateSchoolHolidaySchedule(int Id,SchoolHolidayScheduleDTO schoolHolidayScheduleDTO)
        {
            try
            {
                var sc = await _schoolHolidayScheduleService.UpdateSchoolHolidaySchedule(schoolHolidayScheduleDTO);
                return Ok(sc);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

