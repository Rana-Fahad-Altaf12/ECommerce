using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories.Interfaces.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Subcategory>> GetSubcategoriesAsync(string categoryId);
    }
}
