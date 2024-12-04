using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
              _departmentService = departmentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO departmentDTO)
        {
            var department = await _departmentService.CreateDepartment(departmentDTO);
            return Ok(department);
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var department = await _departmentService.GetAllDepartment();
            return Ok(department);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDepartment(int Id)
        {
            var department = await _departmentService.GetDepartmentById(Id);
            return Ok(department);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDepartment (int Id)
        {
            var department = await _departmentService.DeleteDepartment(Id);
            return Ok(department);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDepartment (int Id, DepartmentDTO departmentDTO)
        {
            if (departmentDTO.DepartmentId != Id)
            {
                return BadRequest("Id don't same");
            }
            var department = await _departmentService.UpdateDepartment(departmentDTO);
            return Ok(department);
        }
    }
