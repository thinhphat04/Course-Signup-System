using API.Dto;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

 [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authService.AuthenticateAdminAsync(request.Username, request.Password);
                if (user == null) return Unauthorized("Invalid admin credentials");

                return Ok(new { Message = "Admin login successful", User = user });
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("login/teacher")]
        public async Task<IActionResult> LoginTeacher([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authService.AuthenticateTeacherAsync(request.Username, request.Password);
                if (user == null) return Unauthorized("Invalid teacher credentials");

                return Ok(new { Message = "Teacher login successful", User = user });
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("login/student")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authService.AuthenticateStudentAsync(request.Username, request.Password);
                if (user == null) return Unauthorized("Invalid student credentials");

                return Ok(new { Message = "Student login successful", User = user });
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var success = await _authService.ResetPasswordAsync(request.UserId, request.NewPassword);
                if (!success) return BadRequest("Failed to reset password");

                return Ok("Password reset successful");
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        
        // [HttpPost("register")]
        // public async Task<IActionResult> RegisterStudent([FromForm] StudentRegistrationDto studentDto)
        // {
        //     try
        //     {
        //         var result = await _studentService.RegisterStudentAsync(studentDto);
        //         if (!result)
        //         {
        //             return BadRequest("Đăng ký không thành công.");
        //         }
        //         return Ok("Đăng ký thành công.");
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log error if necessary
        //         return StatusCode(500, "Đã xảy ra lỗi trong quá trình đăng ký.");
        //     }
        // }
    }