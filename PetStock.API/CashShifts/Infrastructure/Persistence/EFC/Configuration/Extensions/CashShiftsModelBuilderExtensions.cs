using Microsoft.EntityFrameworkCore;
using PetStock.API.CashShifts.Domain.Model.Aggregates;

namespace PetStock.API.CashShifts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the CashShifts bounded context.</summary>
public static class CashShiftsModelBuilderExtensions
{
    public static void ApplyCashShiftsConfiguration(this ModelBuilder builder)
    {
        // cash_shifts
        builder.Entity<CashShift>().HasKey(s => s.Id);
        builder.Entity<CashShift>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CashShift>().Property(s => s.UserId).IsRequired();
        builder.Entity<CashShift>().Property(s => s.OpenedAt).IsRequired();
        builder.Entity<CashShift>().Property(s => s.OpeningAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CashShift>().Property(s => s.ClosingAmount).HasPrecision(10, 2);
        builder.Entity<CashShift>().Property(s => s.Status).IsRequired().HasMaxLength(30);
        builder.Entity<CashShift>().HasIndex(s => s.UserId);

        // cash_reconciliations (FK to cash_shifts)
        builder.Entity<CashReconciliation>().HasKey(r => r.Id);
        builder.Entity<CashReconciliation>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CashReconciliation>().Property(r => r.ExpectedAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CashReconciliation>().Property(r => r.ActualAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CashReconciliation>().Property(r => r.Difference).IsRequired().HasPrecision(10, 2);
        builder.Entity<CashReconciliation>()
            .HasOne<CashShift>()
            .WithMany()
            .HasForeignKey(r => r.CashShiftId)
            .IsRequired();
    }
}
