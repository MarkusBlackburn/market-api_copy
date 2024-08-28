﻿// <auto-generated />
using System;
using Infrastructure.Data.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace market_api.Migrations.Data
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240811011156_Rebuild Project to new version")]
    partial class RebuildProjecttonewversion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int")
                        .HasColumnName("categories_id");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int")
                        .HasColumnName("products_id");

                    b.HasKey("CategoriesId", "ProductsId")
                        .HasName("pk_category_product");

                    b.HasIndex("ProductsId")
                        .HasDatabaseName("ix_category_product_products_id");

                    b.ToTable("category_product");
                });

            modelBuilder.Entity("Core.Models.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSubCategory")
                        .HasColumnType("bit")
                        .HasColumnName("is_sub_category");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url_handle");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Core.Models.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("Discount")
                        .HasColumnType("int")
                        .HasColumnName("discount");

                    b.Property<int>("InternalCode")
                        .HasColumnType("int")
                        .HasColumnName("internal_code");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasColumnName("is_available");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<decimal>("PriceWithDiscount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price_with_discount");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int")
                        .HasColumnName("quantity_in_stock");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url_handle");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Core.Models.Domain.ProductCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.HasKey("Id")
                        .HasName("pk_characteristics");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_characteristics_product_id");

                    b.ToTable("characteristics");
                });

            modelBuilder.Entity("Core.Models.Domain.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("file_extension");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("filename");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image_url");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_images_product_id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("Core.Models.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_product_categories_categories_id");

                    b.HasOne("Core.Models.Domain.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_product_products_products_id");
                });

            modelBuilder.Entity("Core.Models.Domain.ProductCharacteristic", b =>
                {
                    b.HasOne("Core.Models.Domain.Product", "Product")
                        .WithMany("Characteristics")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_characteristics_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Core.Models.Domain.ProductImage", b =>
                {
                    b.HasOne("Core.Models.Domain.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_images_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Core.Models.Domain.Product", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
