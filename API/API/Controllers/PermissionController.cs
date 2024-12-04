using API.DTO;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePermission(PermissionDTO permissionDTO)
        {
            try
            {
                var permission = await _permissionService.CreatePermission(permissionDTO);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPermission()
        {
            try
            {
                var permission = await _permissionService.GetPermission();
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPermission(int Id)
        {
            try
            {
                var permission = await _permissionService.GetPermissionById(Id);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePermission(int Id)
        {
            try
            {
                var permission = await _permissionService.DeletePermission(Id);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult>UpdatePermission(int Id, PermissionDTO permissionDTO)
        {
            if(Id != permissionDTO.Id)
            {
                return BadRequest("Id don't same");
            }
            try
            {
                var permission = await _permissionService.UpdatePermission(permissionDTO);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("CreateRolePermission")]
        public async Task<IActionResult> CreateRolePermission(RolePermissionDTO RolePermission)
        {
            try
            {
                var permission = await _permissionService.CreateRolePermission(RolePermission);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetRolePermission")]
        public async Task<IActionResult> GetRolePermission()
        {
            try
            {
                var permission = await _permissionService.GetRolePermission();
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetRolePermission/{Id}")]
        public async Task<IActionResult> GetRolePermission(int Id)
        {
            try
            {
                var permission = await _permissionService.GetRolePermissionById(Id);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("RolePermission/{Id}")]
        public async Task<IActionResult> DeleteRolePermission(int Id)
        {
            try
            {
                var permission = await _permissionService.DeleteRolePermission(Id);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("RolePermission/{Id}")]
        public async Task<IActionResult> UpdateRolePermission(int Id, RolePermissionDTO RolepermissionDTO)
        {
            if (Id != RolepermissionDTO.Id)
            {
                return BadRequest("Id don't same");
            }
            try
            {
                var permission = await _permissionService.UpdateRolePermission(RolepermissionDTO);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

