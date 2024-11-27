using Ecommerce.DAL.Entities.Authentication;
using Ecommerce.Model.DTOs.Authentication;

namespace Ecommerce.Service.Interfaces.Authentication
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(UserRegisterDto userDto);
        Task<UserResponseDto> LoginAsync(UserLoginDto loginDto);
        Task<ICollection<UserRole>> GetUserRolesAsync(int userId);
        Task<bool> SendPasswordResetEmailAsync(ForgotPasswordDto model);
        Task<bool> ResetPasswordAsync(ResetPasswordDto model);
        Task LogoutAsync();
        Task<string> GeneratePasswordResetToken(User user);
    }
}