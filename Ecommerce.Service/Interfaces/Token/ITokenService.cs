using Ecommerce.DAL.Entities.Authentication;

namespace Ecommerce.Service.Interfaces.Token
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
