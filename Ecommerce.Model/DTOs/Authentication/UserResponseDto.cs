namespace Ecommerce.Model.DTOs.Authentication
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
