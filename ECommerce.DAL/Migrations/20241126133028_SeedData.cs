using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Books" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User " }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MobileNumber", "Password", "PasswordResetToken", "TokenExpiration", "Username" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin", "User ", null, "admin123", null, null, "admin" },
                    { 2, "john@example.com", "John", "Doe", null, "password123", null, null, "john_doe" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Mobile Phones" },
                    { 2, 1, "Laptops" },
                    { 3, 2, "Fiction" },
                    { 4, 2, "Non-Fiction" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "Rating", "SubcategoryId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1830), "Latest Apple smartphone", "iphone13.jpg", "iPhone 13", 999.99m, 4.5, 1 },
                    { 2, 1, new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1851), "High-performance laptop", "macbookpro.jpg", "MacBook Pro", 1999.99m, 4.7000000000000002, 2 },
                    { 3, 2, new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1853), "Classic novel by F. Scott Fitzgerald", "gatsby.jpg", "The Great Gatsby", 10.99m, 4.2999999999999998, 3 },
                    { 4, 2, new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1855), "Non-fiction book by Yuval Noah Harari", "sapiens.jpg", "Sapiens: A Brief History of Humankind", 15.99m, 4.5999999999999996, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
