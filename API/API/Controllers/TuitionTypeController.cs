using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TuitionTypeController : ControllerBase
    {
        private readonly ITuitionTypeService _tuitionTypeService;
        public TuitionTypeController(ITuitionTypeService tuitionTypeService)
        {
            _tuitionTypeService = tuitionTypeService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTuitionType(TuitionTypeDTO tuitionTypeDTO)
        {
            try
            {
                var TuitionType = await _tuitionTypeService.CreateTuitionType(tuitionTypeDTO);
                return Ok(TuitionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTuitionType()
        {
            try
            {
                var TuitionType = await _tuitionTypeService.GetTuitionType();
                return Ok(TuitionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTuitionTypeById(int Id)
        {
            try
            {
                var TuitionType = await _tuitionTypeService.GetTuitionTypeById(Id);
                return Ok(TuitionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTuitionType(int Id)
        {
            try
            {
                var TuitionType = await _tuitionTypeService.DeleteTuitionType(Id);
                return Ok(TuitionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTuitionType(int Id,TuitionTypeDTO tuitionTypeDTO)
        {
            if(Id != tuitionTypeDTO.TuitionTypeId)
            {
                return BadRequest("Id don't Same");
            }
            try
            {
                var TuitionType = await _tuitionTypeService.UpdateTuitionType(Id,tuitionTypeDTO);
                return Ok(TuitionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

