using Microsoft.EntityFrameworkCore;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Entities;

namespace PetStock.API.Sales.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Sales bounded context.</summary>
public static class SalesModelBuilderExtensions
{
    public static void ApplySalesConfiguration(this ModelBuilder builder)
    {
        // sales (one sale has many sale_items)
        builder.Entity<Sale>().HasKey(s => s.Id);
        builder.Entity<Sale>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(s => s.UserId).IsRequired();
        builder.Entity<Sale>().Property(s => s.PaymentMethod).IsRequired().HasMaxLength(40);
        builder.Entity<Sale>().Property(s => s.TotalAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<Sale>().Property(s => s.SaleDate).IsRequired();
        builder.Entity<Sale>().HasIndex(s => s.CustomerId);
        builder.Entity<Sale>()
            .HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(i => i.SaleId)
            .IsRequired();

        // sale_items
        builder.Entity<SaleItem>().HasKey(i => i.Id);
        builder.Entity<SaleItem>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SaleItem>().Property(i => i.ProductId).IsRequired();
        builder.Entity<SaleItem>().Property(i => i.Quantity).IsRequired();
        builder.Entity<SaleItem>().Property(i => i.UnitPrice).IsRequired().HasPrecision(10, 2);

        // payments (FK to sales)
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<Payment>().Property(p => p.PaymentMethod).IsRequired().HasMaxLength(40);
        builder.Entity<Payment>().Property(p => p.PaidAt).IsRequired();
        builder.Entity<Payment>()
            .HasOne<Sale>()
            .WithMany()
            .HasForeignKey(p => p.SaleId)
            .IsRequired();
    }
}
