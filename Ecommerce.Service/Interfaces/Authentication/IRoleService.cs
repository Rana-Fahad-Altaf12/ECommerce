using Ecommerce.DAL.Entities.Authentication;

namespace Ecommerce.Service.Interfaces.Authentication
{
    public interface IRoleService
    {
        Task<ICollection<Role>> GetRolesByIdsAsync(List<int> roleIds);

    }
}
