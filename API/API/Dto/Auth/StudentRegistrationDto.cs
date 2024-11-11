namespace API.Dto;

public class StudentRegistrationDto
{
    public string LastName { get; set; }  // Họ
    public string FirstName { get; set; } // Tên đệm và tên
    public DateTime DateOfBirth { get; set; } // Ngày sinh
    public string Gender { get; set; } // Giới tính
    public string Address { get; set; } // Địa chỉ
    public string Email { get; set; } // Email
    public string PhoneNumber { get; set; } // Số điện thoại
    public string ParentName { get; set; } // Phụ huynh
    public string Password { get; set; } // Mật khẩu
    public IFormFile ProfilePicture { get; set; } // Hình đại diện
}