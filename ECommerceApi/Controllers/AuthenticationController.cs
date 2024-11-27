using Ecommerce.DAL.Entities.Authentication;
using Ecommerce.Model.DTOs.Authentication;
using Ecommerce.Service.Interfaces.Authentication;
using Ecommerce.Service.Interfaces.Token;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService, ITokenService tokenService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> RegisterAsync([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerUser = await _userService.RegisterAsync(user);
            if (registerUser == null)
            {
                return BadRequest(new { ErrorMessage = "User  registration failed." });
            }

            return CreatedAtAction(nameof(RegisterAsync), new { id = registerUser.Id }, registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation($"User  {model.Username} attempted to log in.");
                var user = await _userService.LoginAsync(model);
                if (user == null)
                {
                    return Unauthorized(new { ErrorMessage = "Invalid username or password." });
                }

                User userObj = new User()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    UserRoles = await _userService.GetUserRolesAsync(user.Id),
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                var token = await _tokenService.GenerateTokenAsync(userObj);
                return Ok(new { token, firstName = user.FirstName, lastName = user.LastName });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                return StatusCode(500, new { ErrorMessage = "An internal server error occurred." });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.SendPasswordResetEmailAsync(model);
            if (!result)
            {
                return NotFound(new { ErrorMessage = "User  with this email address does not exist." });
            }

            return Ok(new { Message = "Password reset email sent successfully." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ResetPasswordAsync(model);
            if (!result)
            {
                return BadRequest(new { ErrorMessage = "Invalid token or password reset failed." });
            }

            return Ok(new { Message = "Password reset successfully." });
        }
    }
}