using AutoMapper;
using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public UserController(IUserService userService)
        {
            _userService = userService;  
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            try
            {             
                var user = await _userService.CreateUser(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pagesize =10)
        {
            try
            {
                var users = await _userService.GetUser(page,pagesize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            try
            {
                var user = await _userService.GetUserById(Id);
               
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                var users = await _userService.DeleteUser(Id);
              
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(string Id,UserDTO userDto)
        {
            if(Id != userDto.UserId)
            {
                return BadRequest("id and userid don't");
            }
            try
            {
                var user = await _userService.UpdateUser(userDto);            
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("Get-UserEmail")]
        public async Task<IActionResult> GetUserbyrole(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            return Ok(result);
        }
    }

