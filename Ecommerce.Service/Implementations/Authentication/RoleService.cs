using Ecommerce.DAL.Entities.Authentication;
using Ecommerce.DAL.Repositories.Interfaces.Authentication;
using Ecommerce.Service.Interfaces.Authentication;

namespace Ecommerce.Service.Implementations.Authentication
{
    public class RoleService : IRoleService
    {
        private readonly IUserRepository _repository;

        public RoleService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<Role>> GetRolesByIdsAsync(List<int> roleIds)
        {
            return await _repository.GetRolesByIdsAsync(roleIds);
        }
    }
}
