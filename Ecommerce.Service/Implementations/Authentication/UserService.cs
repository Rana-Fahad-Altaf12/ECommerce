using ECommerceApi.Services.Common;
using System.Security.Cryptography;
using Ecommerce.Service.Interfaces.Authentication;
using Ecommerce.Service.Interfaces.Email;
using Ecommerce.DAL.Repositories.Interfaces.Authentication;
using Ecommerce.Service.Interfaces.Token;
using Ecommerce.Model.DTOs.Authentication;
using Ecommerce.DAL.Entities.Authentication;

namespace ECommerceApi.Services.Implementations.Authentication
{
    public class UserService : IUserService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<UserResponseDto> RegisterAsync(UserRegisterDto userDto)
        {
            int userId = 0;
            try
            {
                if (userDto.RoleIds == null || userDto.RoleIds.Count() == 0)
                {
                    userDto.RoleIds = userDto.RoleIds == null ? new List<int>() : userDto.RoleIds;
                    userDto.RoleIds.Add(ConstantKeys.UserRole);
                }
                var roles = await _userRepository.GetRolesByIdsAsync(userDto.RoleIds);

                var user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    UserRoles = new List<UserRole>()
                };

                await _userRepository.CreateUserAsync(user);

                userId = user.Id;
                user.UserRoles = roles.Select(role => new UserRole { RoleId = role.Id, UserId = user.Id }).ToList();

                await _userRepository.AddUserRolesAsync(user.UserRoles);

                return new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(x => x.Role.Name).ToList(),
                    StatusCode = ConstantKeys.OK,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            catch (Exception ex)
            {
                var user = await _userRepository.GetUserByIdsAsync(userId);
                if (user != null)
                {
                    await _userRepository.DeleteUserAsync(user);
                }
                return new UserResponseDto
                {
                    StatusCode = ConstantKeys.BadRequest,
                    ErrorMessage = ex.Message
                };
            }

        }

        public async Task<UserResponseDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token;
            try
            {
                token = await _tokenService.GenerateTokenAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating token: {ex.Message}");
                throw new ApplicationException("An error occurred while generating the token.");
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        public async Task<ICollection<UserRole>> GetUserRolesAsync(int userId)
        {
            return await _userRepository.GetUserRolesAsync(userId);
        }
        public async Task LogoutAsync()
        {
        }
        public async Task<bool> SendPasswordResetEmailAsync(ForgotPasswordDto model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }

            var token = GeneratePasswordResetToken(user);
            user.PasswordResetToken = token.ToString();
            user.TokenExpiration = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateAsync(user);

            var resetLink = $"http://localhost:4200/reset-password?token={token}";
            bool emailSent = await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Reset your password using this link: {resetLink}");

            return emailSent;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userRepository.GetUserByResetTokenAsync(model.Token);
            if (user == null || user.TokenExpiration < DateTime.UtcNow)
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.PasswordResetToken = null;
            user.TokenExpiration = null;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            string token;
            bool isUnique = false;

            do
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] tokenData = new byte[32];
                    rng.GetBytes(tokenData);
                    token = Convert.ToBase64String(tokenData)
                        .Replace("+", "-")
                        .Replace("/", "_")
                        .Replace("=", "");
                }

                isUnique = await _userRepository.IsTokenUniqueAsync(token);
            } while (!isUnique);

            return token;
        }

    }
}