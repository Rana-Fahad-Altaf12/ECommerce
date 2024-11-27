using Ecommerce.DAL.Entities.Authentication;

namespace Ecommerce.Model.DTOs.Authentication
{
    public class UserRoleResponseDto
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
