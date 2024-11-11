namespace API.Dto;

public class ResetPasswordRequest
{
    public int UserId { get; set; }
    public string NewPassword { get; set; }
}