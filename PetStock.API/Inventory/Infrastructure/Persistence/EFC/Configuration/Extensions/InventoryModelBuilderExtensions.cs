using Microsoft.EntityFrameworkCore;
using PetStock.API.Inventory.Domain.Model.Aggregates;

namespace PetStock.API.Inventory.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Inventory bounded context.</summary>
public static class InventoryModelBuilderExtensions
{
    public static void ApplyInventoryConfiguration(this ModelBuilder builder)
    {
        // product_stocks (one per product)
        builder.Entity<ProductStock>().HasKey(s => s.Id);
        builder.Entity<ProductStock>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProductStock>().Property(s => s.ProductId).IsRequired();
        builder.Entity<ProductStock>().Property(s => s.CurrentStock).IsRequired();
        builder.Entity<ProductStock>().Property(s => s.MinimumStock).IsRequired();
        builder.Entity<ProductStock>().HasIndex(s => s.ProductId).IsUnique();

        // stock_alerts
        builder.Entity<StockAlert>().HasKey(a => a.Id);
        builder.Entity<StockAlert>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<StockAlert>().Property(a => a.ProductId).IsRequired();
        builder.Entity<StockAlert>().Property(a => a.AlertLevel).IsRequired().HasMaxLength(40);
        builder.Entity<StockAlert>().Property(a => a.IsActive).IsRequired();
        builder.Entity<StockAlert>().HasIndex(a => a.ProductId);
    }
}
