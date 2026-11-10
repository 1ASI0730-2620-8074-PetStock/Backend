using Microsoft.EntityFrameworkCore;
using PetStock.API.Analytics.Domain.Model.Aggregates;

namespace PetStock.API.Analytics.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Analytics bounded context.</summary>
public static class AnalyticsModelBuilderExtensions
{
    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        // reports
        builder.Entity<Report>().HasKey(r => r.Id);
        builder.Entity<Report>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(r => r.UserId).IsRequired();
        builder.Entity<Report>().Property(r => r.ReportType).IsRequired().HasMaxLength(40);
        builder.Entity<Report>().Property(r => r.FileFormat).IsRequired().HasMaxLength(20);
        builder.Entity<Report>().Property(r => r.GeneratedAt).IsRequired();
        builder.Entity<Report>().HasIndex(r => r.UserId);
    }
}
