using Ecommerce.DAL.Entities.Authentication;
using Ecommerce.DAL.Repositories.Interfaces.Authentication;
using ECommerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL.Repositories.Implementation.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDbContext _context;

        public UserRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                      .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByIdsAsync(int userId)
        {
            return await _context.Users
                      .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserRole>> GetUserRolesAsync(int userId)
        {
            var user = await _context.Users
                      .Include(u => u.UserRoles)
                      .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.ToList() ?? new List<UserRole>();
        }

        public async Task<ICollection<Role>> GetRolesByIdsAsync(List<int> roleIds)
        {
            return await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .ToListAsync();
        }

        public async Task AddUserRolesAsync(ICollection<UserRole> userRoles)
        {
            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByResetTokenAsync(string token)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.TokenExpiration > DateTime.UtcNow);
        }
        public async Task<bool> IsTokenUniqueAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
            return user == null;
        }
    }
}
