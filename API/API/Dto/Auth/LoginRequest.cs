namespace API.Dto;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Thêm trường Role để phân biệt quyền
}