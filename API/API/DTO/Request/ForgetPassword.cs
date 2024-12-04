using System.ComponentModel.DataAnnotations;

namespace API.DTO.Request
{
    public class ForgetPassword
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

    }
}
