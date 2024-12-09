using API.Entities;
using API.DTO.Request;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AuthRepository : IAuthService
    {
        private readonly CourseSystemDB _dbcontext; 
        private readonly IHashPasword _hashPasword; 
        private readonly IGenerateService _generateService; 

        public AuthRepository(CourseSystemDB dbcontext, IHashPasword hashPasword, IGenerateService generateService)
        {
            _dbcontext = dbcontext;
            _hashPasword = hashPasword;
            _generateService = generateService;
        }

        // Xử lý quên mật khẩu
        public async Task<string> ForgetPassword(ForgetPassword ForgetPassword)
        {
            // Kiểm tra xem email có tồn tại trong hệ thống không
            var email = await _dbcontext.Users.AnyAsync(u => u.Email == ForgetPassword.Email);
            if (email == true)
            {
                // Nếu email tồn tại, gửi email xác thực
                var sendemail = await _generateService.SendEmail(ForgetPassword.Email);
                if (sendemail == true)
                {
                    // Nếu email được gửi thành công, tạo token xác thực
                    var jwt = await _generateService.GenerateVerificationToken(ForgetPassword.Email);
                    return jwt; // Trả về token
                }
                throw new Exception($"Send email didn't succeed: {sendemail}"); // Thông báo lỗi nếu gửi email không thành công
            }
            else
            {
                throw new Exception("Email is incorrect"); // Thông báo lỗi nếu email không tồn tại
            }
        }

        // Xử lý đăng nhập
        public async Task<string> Login(Login login)
        {
            if (login.ConfirmTeacher == true) // Kiểm tra nếu đăng nhập là giáo viên
            {
                // Tìm kiếm giáo viên theo email
                var teacher = await _dbcontext.Users.FirstOrDefaultAsync(t => t.Email == login.Username);
                if (teacher is null) return "User name is incorrect"; // Không tìm thấy tài khoản giáo viên
                else
                {
                    // Kiểm tra mật khẩu bằng cách giải mã và xác minh
                    if (!_hashPasword.VerifyHashPassword(login.Password, teacher.PasswordHash, teacher.PasswordSalt))
                    {
                        return "Password is incorrect"; // Mật khẩu không khớp
                    }

                    // Tạo token JWT và trả về
                    var jwt = await _generateService.GenerateJwtToken(teacher);
                    return jwt;
                }
            }
            else // Xử lý trường hợp đăng nhập là học sinh
            {
                // Tìm kiếm học sinh theo mã username (giả định username là mã học sinh)
                var student = await _dbcontext.Users.FindAsync(login.Username);
                if (student is null) return "Code is incorrect"; // Không tìm thấy tài khoản học sinh
                else
                {
                    // Kiểm tra mật khẩu học sinh
                    if (!_hashPasword.VerifyHashPassword(login.Password, student.PasswordHash, student.PasswordSalt))
                    {
                        return "Password is incorrect"; // Mật khẩu không khớp
                    }

                    // Tạo token JWT và trả về
                    var jwt = await _generateService.GenerateJwtToken(student);
                    return jwt;
                }
            }
        }
    }
}