using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Entities.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
