using AutoMapper;
using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
      
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole ([FromBody] RoleDTO roleDTO)
        {
            try
            {
                var role = await _roleService.CreateRole(roleDTO);               
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetRoles();               
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoleById (int Id)
        {
            try
            {
                var role = await _roleService.GetRoleById(Id);
              
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            try
            {
                var role = await _roleService.DeleteRole(Id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRole(int Id,RoleDTO roleDTO)
        {
            if(roleDTO.RoleId != Id)
            {
                return BadRequest("id and roleid incorrect");
            }
            try{
               
                var role = await _roleService.UpdateRole(roleDTO);               
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

