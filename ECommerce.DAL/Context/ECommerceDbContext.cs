using Ecommerce.DAL.Entities.Authentication;
using Ecommerce.DAL.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User " }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@example.com", Password = "admin123", FirstName = "Admin", LastName = "User " },
                new User { Id = 2, Username = "john_doe", Email = "john@example.com", Password = "password123", FirstName = "John", LastName = "Doe" }
            );

            // Seed UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1 }, // Admin role for admin user
                new UserRole { Id = 2, UserId = 2, RoleId = 2 }  // User role for John Doe
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" }
            );

            // Seed Subcategories
            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory { Id = 1, Name = "Mobile Phones", CategoryId = 1 },
                new Subcategory { Id = 2, Name = "Laptops", CategoryId = 1 },
                new Subcategory { Id = 3, Name = "Fiction", CategoryId = 2 },
                new Subcategory { Id = 4, Name = "Non-Fiction", CategoryId = 2 }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "iPhone 13", Description = "Latest Apple smartphone", Price = 999.99m, ImageUrl = "iphone13.jpg", Rating = 4.5, CreatedAt = DateTime.Now, CategoryId = 1, SubcategoryId = 1 },
                new Product { Id = 2, Name = "MacBook Pro", Description = "High-performance laptop", Price = 1999.99m, ImageUrl = "macbookpro.jpg", Rating = 4.7, CreatedAt = DateTime.Now, CategoryId = 1, SubcategoryId = 2 },
                new Product { Id = 3, Name = "The Great Gatsby", Description = "Classic novel by F. Scott Fitzgerald", Price = 10.99m, ImageUrl = "gatsby.jpg", Rating = 4.3, CreatedAt = DateTime.Now, CategoryId = 2, SubcategoryId = 3 },
                new Product { Id = 4, Name = "Sapiens: A Brief History of Humankind", Description = "Non-fiction book by Yuval Noah Harari", Price = 15.99m, ImageUrl = "sapiens.jpg", Rating = 4.6, CreatedAt = DateTime.Now, CategoryId = 2, SubcategoryId = 4 }
            );
        }
    }
}