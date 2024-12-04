
using API.Entities;
using API.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Repositories
{
    public class GenerateRepository : IGenerateService
    {
        private readonly CourseSystemDB _dbContext;
        private readonly IConfiguration _configuration;
        public GenerateRepository(CourseSystemDB dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<string> GenerateCodeAsync()
        {
           var year = DateTime.Now.Year;
           var month = DateTime.Now.Month.ToString("D2");
           int nextNumber = 1;
            var prefix = $"{ year}{month}";
            var lastCode = await _dbContext.Users
                                   .Where(u => u.UserId.StartsWith(prefix))
                                   .OrderByDescending(u => u.UserId)
                                   .Select(u => u.UserId)
                                   .FirstOrDefaultAsync();
            if (lastCode != null)
            {
                // Tách phần số cuối cùng từ mã
                var lastNumber = int.Parse(lastCode.Split('-').Last());
                nextNumber = lastNumber + 1;
            }
            return $"{prefix}-{nextNumber.ToString("D6")}";
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var role = await _dbContext.Users.Include(u => u.Role).Where(u =>u.UserId == user.UserId).FirstOrDefaultAsync();
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role!.Role.RoleName),
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim("RoleId", user.RoleId.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims,
                            signingCredentials: credentials,
                            expires: DateTime.Now.AddMinutes(30));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<string> GenerateVerificationToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("Permission","VerificationToken")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims,
                            signingCredentials: credentials,
                            expires: DateTime.Now.AddMinutes(30));
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async  Task<bool> SendEmail(string email)
        {
            try
            {
                var Email = new MimeMessage();
                // email gui noi dung
                Email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
                //email nhan noi dung
                Email.To.Add(MailboxAddress.Parse(email));
                Email.Subject = "your verification code";
                Random random = new Random();
                string verification = random.Next(100000, 999999).ToString();
                Email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = $"your verification code: {verification}"
                };
                var emailverification = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (emailverification == null)
                {
                    return false;
                }
                emailverification.VerificationCode = verification;
                await _dbContext.SaveChangesAsync();
                using (var smpt = new SmtpClient())
                {
                    await smpt.ConnectAsync(_configuration.GetSection("Email:Host").Value, 587, SecureSocketOptions.StartTls);
                    await smpt.AuthenticateAsync(_configuration.GetSection("Email:UserName").Value,_configuration.GetSection("Email:Password").Value);

                    await smpt.SendAsync(Email);
                    await smpt.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> VerificationToken(string email, string Token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            else if (user.VerificationCode == Token)
            {
                // xác nhận đúng thì gán giá trị null vào vì sau này còn sử dụng
                user.VerificationCode = null;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
