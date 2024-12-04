using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGradeTypeController : ControllerBase
    {
        private readonly ISubjectGradeTypeService _subjectGradeTypeService;
        public SubjectGradeTypeController(ISubjectGradeTypeService subjectTypeService)
        {
            _subjectGradeTypeService = subjectTypeService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubjectGradeType(SubjectGradeTypeDTO subjectGradeTypeDTO)
        {
            try
            {
                var grade = await _subjectGradeTypeService.CreateSubjectGradeType(subjectGradeTypeDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectGradeType([FromQuery]int page = 1, [FromQuery] int pagesize =10)
        {
            try
            {
                var grade = await _subjectGradeTypeService.GetSubjectGradeType(page, pagesize);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSubjectGradeTypeById(int Id)
        {
            try
            {
                var grade = await _subjectGradeTypeService.GetSubjectGradeTypeById(Id);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubjectGradeType(int Id)
        {
            try
            {
                var grade = await _subjectGradeTypeService.DeleteSubjectGradeTypeType(Id);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> GetSubjectGradeType(int Id, SubjectGradeTypeDTO gradeDTO)
        {
            try
            {
                var grade = await _subjectGradeTypeService.UpdateSubjectGradeType(Id, gradeDTO);
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

