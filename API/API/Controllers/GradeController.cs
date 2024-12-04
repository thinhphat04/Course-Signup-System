using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGrade(GradeDTO gradeDTO)
        {
            try
            {
                var grade = await _gradeService.CreateGrade(gradeDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGrade()
        {
            try
            {
                var grade = await _gradeService.GetGrade();
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
                var grade = await _gradeService.GetGradeById(Id);
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
                var grade = await _gradeService.DeleteGradeType(Id);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> GetGrade(int Id, GradeDTO gradeDTO)
        {
            try
            {
                var grade = await _gradeService.UpdateGrade(Id, gradeDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetAcademicTranscript")]
        public async Task<IActionResult> GetAcademicTranscript()
        {
            try { 
                var grade = await _gradeService.GetAcademicTranscript();
                return Ok(grade);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Get-AcademicTranscriptByStudent")]
        public async Task<IActionResult> GetAcademicTranscriptByStudent(string StudentId)
        {
            try
            {
                var grade = await _gradeService.GetAcademicTranscriptByStudent(StudentId);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetGradeByGradeType")]
        public async Task<IActionResult> GetGradeByGradeType( int GradeTypeId, string StudentId)
        {
            try
            {
                var grade = await _gradeService.GetGradeByGradeType(GradeTypeId, StudentId);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

