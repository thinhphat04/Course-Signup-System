using System.ComponentModel.DataAnnotations;

namespace API.DTO.Request
{
    public class Login
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public bool ConfirmTeacher { get; set; } = false;

    }
}
