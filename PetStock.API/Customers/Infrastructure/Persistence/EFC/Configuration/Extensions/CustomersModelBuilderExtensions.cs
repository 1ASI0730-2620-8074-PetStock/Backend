using Microsoft.EntityFrameworkCore;
using PetStock.API.Customers.Domain.Model.Aggregates;

namespace PetStock.API.Customers.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Customers bounded context.</summary>
public static class CustomersModelBuilderExtensions
{
    public static void ApplyCustomersConfiguration(this ModelBuilder builder)
    {
        // customers
        builder.Entity<Customer>().HasKey(c => c.Id);
        builder.Entity<Customer>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Customer>().Property(c => c.FullName).IsRequired().HasMaxLength(150);
        builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(150);
        builder.Entity<Customer>().Property(c => c.Phone).IsRequired().HasMaxLength(30);
        builder.Entity<Customer>().Property(c => c.Address).IsRequired().HasMaxLength(255);

        // purchase_history_entries (FK to customers)
        builder.Entity<PurchaseHistoryEntry>().HasKey(e => e.Id);
        builder.Entity<PurchaseHistoryEntry>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PurchaseHistoryEntry>().Property(e => e.SaleId).IsRequired();
        builder.Entity<PurchaseHistoryEntry>().Property(e => e.TotalAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<PurchaseHistoryEntry>().Property(e => e.PurchasedAt).IsRequired();
        builder.Entity<PurchaseHistoryEntry>().HasIndex(e => e.SaleId);
        builder.Entity<PurchaseHistoryEntry>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(e => e.CustomerId)
            .IsRequired();
    }
}
