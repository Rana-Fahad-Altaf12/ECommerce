﻿// <auto-generated />
using System;
using ECommerce.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.DAL.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20241126133028_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User "
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@example.com",
                            FirstName = "Admin",
                            LastName = "User ",
                            Password = "admin123",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "john@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "password123",
                            Username = "john_doe"
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Books"
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1830),
                            Description = "Latest Apple smartphone",
                            ImageUrl = "iphone13.jpg",
                            Name = "iPhone 13",
                            Price = 999.99m,
                            Rating = 4.5,
                            SubcategoryId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1851),
                            Description = "High-performance laptop",
                            ImageUrl = "macbookpro.jpg",
                            Name = "MacBook Pro",
                            Price = 1999.99m,
                            Rating = 4.7000000000000002,
                            SubcategoryId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1853),
                            Description = "Classic novel by F. Scott Fitzgerald",
                            ImageUrl = "gatsby.jpg",
                            Name = "The Great Gatsby",
                            Price = 10.99m,
                            Rating = 4.2999999999999998,
                            SubcategoryId = 3
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2024, 11, 26, 18, 30, 27, 682, DateTimeKind.Local).AddTicks(1855),
                            Description = "Non-fiction book by Yuval Noah Harari",
                            ImageUrl = "sapiens.jpg",
                            Name = "Sapiens: A Brief History of Humankind",
                            Price = 15.99m,
                            Rating = 4.5999999999999996,
                            SubcategoryId = 4
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Mobile Phones"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Name = "Laptops"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Name = "Non-Fiction"
                        });
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.UserRole", b =>
                {
                    b.HasOne("Ecommerce.DAL.Entities.Authentication.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.DAL.Entities.Authentication.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Product", b =>
                {
                    b.HasOne("Ecommerce.DAL.Entities.Products.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.DAL.Entities.Products.Subcategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Subcategory", b =>
                {
                    b.HasOne("Ecommerce.DAL.Entities.Products.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Authentication.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce.DAL.Entities.Products.Subcategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}