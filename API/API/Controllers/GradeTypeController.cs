using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTypeController : ControllerBase
    {
        private readonly IGradeTypeService _gradeTypeService;
        public GradeTypeController(IGradeTypeService gradeTypeService)
        {
            _gradeTypeService = gradeTypeService;
        }
        [HttpPost] 
        public async Task<IActionResult> CreateGradeType (GradeTypeDTO gradeTypeDTO)
        {
            try
            {
                var gradeType = await _gradeTypeService.CreateGradeType(gradeTypeDTO);
                return Ok(gradeType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGradeType([FromQuery]int page = 1, [FromQuery] int pagesize = 10)
        {
            try
            {
                var gradeType = await _gradeTypeService.GetGradeType(page, pagesize);
                return Ok(gradeType);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetGradeTypeById(int Id)
        {
            try
            {
                var gradeType = await _gradeTypeService.GetGradeTypeById(Id);
                return Ok(gradeType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGradeType(int Id)
        {
            try
            {
                var gradeType = await _gradeTypeService.DeleteGradeType(Id);
                return Ok(gradeType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateGradeType(int Id, GradeTypeDTO gradeTypeDTO)
        {
            try
            {
                var gradeType = await _gradeTypeService.UpdateGradeType(Id,gradeTypeDTO);
                return Ok(gradeType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

