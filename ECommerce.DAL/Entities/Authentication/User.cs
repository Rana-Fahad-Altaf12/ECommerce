using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DAL.Entities.Authentication
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}
