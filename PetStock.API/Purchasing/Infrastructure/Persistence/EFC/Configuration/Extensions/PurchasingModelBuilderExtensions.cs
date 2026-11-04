using Microsoft.EntityFrameworkCore;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Entities;

namespace PetStock.API.Purchasing.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Purchasing bounded context.</summary>
public static class PurchasingModelBuilderExtensions
{
    public static void ApplyPurchasingConfiguration(this ModelBuilder builder)
    {
        // purchase_orders (one order has many purchase_order_items)
        builder.Entity<PurchaseOrder>().HasKey(o => o.Id);
        builder.Entity<PurchaseOrder>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PurchaseOrder>().Property(o => o.SupplierId).IsRequired();
        builder.Entity<PurchaseOrder>().Property(o => o.OrderDate).IsRequired();
        builder.Entity<PurchaseOrder>().Property(o => o.Status).IsRequired().HasMaxLength(30);
        builder.Entity<PurchaseOrder>().HasIndex(o => o.SupplierId);
        builder.Entity<PurchaseOrder>()
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(i => i.PurchaseOrderId)
            .IsRequired();

        // purchase_order_items
        builder.Entity<PurchaseOrderItem>().HasKey(i => i.Id);
        builder.Entity<PurchaseOrderItem>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PurchaseOrderItem>().Property(i => i.ProductId).IsRequired();
        builder.Entity<PurchaseOrderItem>().Property(i => i.Quantity).IsRequired();
        builder.Entity<PurchaseOrderItem>().Property(i => i.UnitCost).IsRequired().HasPrecision(10, 2);

        // goods_receipts (FK to purchase_orders)
        builder.Entity<GoodsReceipt>().HasKey(g => g.Id);
        builder.Entity<GoodsReceipt>().Property(g => g.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<GoodsReceipt>().Property(g => g.ReceivedAt).IsRequired();
        builder.Entity<GoodsReceipt>().Property(g => g.Notes).IsRequired().HasMaxLength(255);
        builder.Entity<GoodsReceipt>()
            .HasOne<PurchaseOrder>()
            .WithMany()
            .HasForeignKey(g => g.PurchaseOrderId)
            .IsRequired();
    }
}
