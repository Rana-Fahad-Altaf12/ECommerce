using Ecommerce.DAL.Entities.Products;
using ECommerce.DAL.Repositories.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories.Implementation.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Subcategory).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Subcategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync(string categoryId)
        {
            return await _context.Subcategories.Where(sc => sc.CategoryId.ToString() == categoryId).ToListAsync();
        }
    }
}
