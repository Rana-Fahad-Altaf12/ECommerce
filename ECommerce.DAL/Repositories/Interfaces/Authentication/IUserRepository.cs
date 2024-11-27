using Ecommerce.DAL.Entities.Authentication;

namespace Ecommerce.DAL.Repositories.Interfaces.Authentication
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User> GetUserByIdsAsync(int userId);

        Task<ICollection<UserRole>> GetUserRolesAsync(int userId);
        Task<ICollection<Role>> GetRolesByIdsAsync(List<int> roleIds);
        Task AddUserRolesAsync(ICollection<UserRole> userRoles);
        Task UpdateAsync(User user);
        Task<User> GetUserByResetTokenAsync(string token);
        Task<bool> IsTokenUniqueAsync(string token);
    }
}
