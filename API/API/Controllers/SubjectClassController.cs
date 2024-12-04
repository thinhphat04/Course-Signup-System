using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectClassController : ControllerBase
    {
        private  readonly ISubjectClassService _subjectClassService;
        public SubjectClassController(ISubjectClassService subjectClassService)
        {
            _subjectClassService = subjectClassService;
        }
        [HttpPost]
        public async Task<IActionResult >CreateSubjectClass([FromBody]SubjectClassDTO subjectClassDTO)
        {
            try
            {
                var subjectClass = await _subjectClassService.CreateSubjectClass(subjectClassDTO);
                return Ok(subjectClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectClass([FromQuery]int page, [FromQuery] int pagesize)
        {
            try
            {
                var subjectClass = await _subjectClassService.GetSubjectClass(page, pagesize);
                return Ok(subjectClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSubjectClassById(int Id)
        {
            try
            {
                var subjectClass = await _subjectClassService.GetSubjectClassById(Id);
                return Ok(subjectClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteSujectClass(int Id)
        {
            try
            {
                var subjectClass = await _subjectClassService.DeleteSubjectClass(Id);
                return Ok(subjectClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubjectClass(int Id,SubjectClassDTO subjectClassDTO)
        {   
            if(Id != subjectClassDTO.Id)
            {
                return BadRequest("Id and subjectClassId don't same");
            }
            try
            {
                var subjectClass = await _subjectClassService.UpdateSubjectClass(Id,subjectClassDTO);
                return Ok(subjectClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

