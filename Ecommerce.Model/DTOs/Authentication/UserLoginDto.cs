using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.DTOs.Authentication
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
