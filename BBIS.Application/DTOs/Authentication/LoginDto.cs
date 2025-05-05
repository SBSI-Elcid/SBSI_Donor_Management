using System.ComponentModel.DataAnnotations;

namespace BBIS.Application.DTOs.Authentication
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
