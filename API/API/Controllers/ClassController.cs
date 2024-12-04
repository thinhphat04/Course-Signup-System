using API.DTO;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassDTO classDTO)
        {
            try
            {
                var Class = await _classService.CreateClass(classDTO);
                return Ok(Class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClass([FromQuery]int page, [FromQuery] int pagesize =10) 
        {
            try
            {
                var Classes = await _classService.GetAllClass(page, pagesize);
                return Ok(Classes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClassById(string Id)
        {
            try
            {
                var Class = await _classService.GetClassById(Id);
                return Ok(Class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClass(string Id)
        {
            try
            {
                var Class = await _classService.DeleteClass(Id);
                return Ok(Class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateClass(string Id, ClassDTO classDTO)
        {
            if (Id != classDTO.ClassId) return BadRequest("Id and classid incorrect");
            try
            {
                var Class = await _classService.DeleteClass(Id);
                return Ok(Class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

