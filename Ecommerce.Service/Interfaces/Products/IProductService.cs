using Ecommerce.Model.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(string category, string subcategory, string searchTerm, string sortBy, int page, int pageSize);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<IEnumerable<SubcategoryDto>> GetSubcategoriesAsync(string categoryId);
    }
}
