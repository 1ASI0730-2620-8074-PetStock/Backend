using Microsoft.EntityFrameworkCore;
using PetStock.API.Catalog.Domain.Model.Aggregates;

namespace PetStock.API.Catalog.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Catalog bounded context.</summary>
public static class CatalogModelBuilderExtensions
{
    public static void ApplyCatalogConfiguration(this ModelBuilder builder)
    {
        // categories
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Category>().Property(c => c.Description).IsRequired().HasMaxLength(255);
        builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        // suppliers
        builder.Entity<Supplier>().HasKey(s => s.Id);
        builder.Entity<Supplier>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Supplier>().Property(s => s.CompanyName).IsRequired().HasMaxLength(150);
        builder.Entity<Supplier>().Property(s => s.ContactName).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(s => s.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<Supplier>().Property(s => s.Email).IsRequired().HasMaxLength(150);

        // products (FK to categories and suppliers)
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(255);
        builder.Entity<Product>().Property(p => p.BasePrice).IsRequired().HasPrecision(10, 2);
        builder.Entity<Product>().Property(p => p.IsActive).IsRequired();
        builder.Entity<Product>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();
        builder.Entity<Product>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(p => p.SupplierId)
            .IsRequired();
    }
}
