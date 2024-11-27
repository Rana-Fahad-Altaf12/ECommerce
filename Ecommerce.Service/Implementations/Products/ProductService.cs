using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interfaces.Products;
using ECommerce.DAL.Repositories.Interfaces.Products;
using Ecommerce.Model.DTOs.Products;

namespace Ecommerce.Service.Implementations.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string category, string subcategory, string searchTerm, string sortBy, int page, int pageSize)
        {
            var products = await _productRepository.GetAllAsync();

            // Apply filters
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(subcategory))
            {
                products = products.Where(p => p.Subcategory.Name == subcategory);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            // Sorting logic
            products = sortBy switch
            {
                "price" => products.OrderBy(p => p.Price),
                "popularity" => products.OrderByDescending(p => p.Rating),
                "newest" => products.OrderByDescending(p => p.CreatedAt),
                _ => products.OrderByDescending(p => p.Rating)
            };

            // Pagination
            var result = _mapper.Map<IEnumerable<ProductDto>>(products.Skip((page - 1) * pageSize).Take(pageSize));
            return result;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _productRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<SubcategoryDto>> GetSubcategoriesAsync(string categoryId)
        {
            var subcategories = await _productRepository.GetSubcategoriesAsync(categoryId);
            return _mapper.Map<IEnumerable<SubcategoryDto>>(subcategories);
        }
    }
}
