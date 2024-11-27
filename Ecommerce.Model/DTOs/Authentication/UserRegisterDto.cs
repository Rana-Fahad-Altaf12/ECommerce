using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.DTOs.Authentication
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
