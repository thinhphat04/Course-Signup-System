using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubject([FromQuery]int page =1, [FromQuery] int pagesize =10)
        {
            try
            {
                var subject = await _subjectService.GetAllSubject(page, pagesize);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSubjectById(string Id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectById(Id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDTO)
        {
            try
            {
                var subject = await _subjectService.CreateSubject(subjectDTO);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubject(string Id)
        {
            try
            {
                var subject = await _subjectService.DeleteSubject(Id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(string Id,SubjectDTO subjectDTO)
        {
            if(Id != subjectDTO.SubjectId)
            {
                return BadRequest("Id and SubjectId don't same");
            }
            try
            {
                var subject = await _subjectService.UpdateSubject(subjectDTO);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetSubjectByName(string name)
        {
            try
            {
                var subject = await _subjectService.SearchSubjectByName(name);
                return Ok(subject);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Department-Faculty")]
        public async Task<IActionResult> GetSubjectByFaculty([FromQuery]int DepartmentId, [FromQuery] string FacultyId)
        {
            try
            {
                var subject = await _subjectService.SearchSubject(DepartmentId,FacultyId);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
