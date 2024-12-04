using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class GradeColumnController : ControllerBase
    { 
        private readonly IGradeColumnService _gradeColumnService;
        public GradeColumnController(IGradeColumnService gradeColumnService)
        {
            _gradeColumnService = gradeColumnService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGradeColumn(GradeColumnDTO gradeColumnDTO)
        {
            try
            {
                var grade = await _gradeColumnService.CreateGrade(gradeColumnDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGradeColumn()
        {
            try
            {
                var grade = await _gradeColumnService.GetGrade();
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetGradeById(int Id)
        {
            try
            {
                var grade = await _gradeColumnService.GetGradeById(Id);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGrade(int Id)
        {
            try
            {
                var grade = await _gradeColumnService.DeleteGradeType(Id);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> GetGrade(int Id, GradeColumnDTO gradeColumnDTO)
        {
            try
            {
                var grade = await _gradeColumnService.UpdateGrade(Id, gradeColumnDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

