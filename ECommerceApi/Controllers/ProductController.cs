using Ecommerce.Model.DTOs.Products;
using Ecommerce.Service.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(
            [FromQuery] string? category = null,
            [FromQuery] string? subcategory = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                sortBy ??= "popularity";
                var products = await _productService.GetProductsAsync(category, subcategory, searchTerm, sortBy, page, pageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                return StatusCode(500, new { ErrorMessage = "An internal server error occurred." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { ErrorMessage = "Product not found." });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product.");
                return StatusCode(500, new { ErrorMessage = "An internal server error occurred." });
            }
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _productService.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories.");
                return StatusCode(500, new { ErrorMessage = "An internal server error occurred." });
            }
        }

        [HttpGet("subcategories")]
        public async Task<ActionResult<IEnumerable<SubcategoryDto>>> GetSubcategoriesAsync(string categoryId)
        {
            try
            {
                var subcategories = await _productService.GetSubcategoriesAsync(categoryId);
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving subcategories.");
                return StatusCode(500, new { ErrorMessage = "An internal server error occurred." });
            }
        }
    }
}