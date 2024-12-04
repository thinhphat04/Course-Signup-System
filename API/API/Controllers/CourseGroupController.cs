using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class CourseGroupController : ControllerBase
    {
        private readonly ICourseGroupService _CourseGroupService;
        public CourseGroupController (ICourseGroupService CourseGroupService)
        {
            _CourseGroupService = CourseGroupService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourseGroup([FromBody] CourseGroupDto CourseGroupDTO)
        {
            try
            {
                var CourseGroup = await _CourseGroupService.CreateCourseGroup(CourseGroupDTO);
                return Ok(CourseGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseGroup([FromQuery]int page = 1, [FromQuery] int pagesize = 10)
        {
            try
            {
                var CourseGroups = await _CourseGroupService.GetAllCourseGroup(page, pagesize);
                return Ok(CourseGroups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseGroup(string Id)
        {
            try
            {
                var CourseGroup = await _CourseGroupService.GetCourseGroupById(Id);
                return Ok(CourseGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCourseGroup(string Id)
        {
            try
            {
                var CourseGroup = await _CourseGroupService.DeleteCourseGroup(Id);
                return Ok(CourseGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> DeleteCourseGroup(string Id,CourseGroupDto CourseGroupDTO)
        {
            if(CourseGroupDTO.CourseGroupId != Id)
            {
                return BadRequest("Id and CourseGroupId incorrect");
            }
            try
            {
                var CourseGroup = await _CourseGroupService.UpdateCourseGroup(CourseGroupDTO);
                return Ok(CourseGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

