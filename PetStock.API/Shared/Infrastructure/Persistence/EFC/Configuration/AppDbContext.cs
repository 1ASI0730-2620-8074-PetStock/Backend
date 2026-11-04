using Microsoft.EntityFrameworkCore;
using PetStock.API.Analytics.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.CashShifts.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Catalog.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Customers.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Inventory.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Purchasing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Sales.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Interceptors;

namespace PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>Database context shared by all bounded contexts.</summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Fill audit dates automatically
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Tables of each bounded context
        builder.ApplyIamConfiguration();
        builder.ApplyProfilesConfiguration();
        builder.ApplyCatalogConfiguration();
        builder.ApplyInventoryConfiguration();
        builder.ApplySalesConfiguration();
        builder.ApplyCustomersConfiguration();
        builder.ApplyPurchasingConfiguration();
        builder.ApplyCashShiftsConfiguration();
        builder.ApplyAnalyticsConfiguration();

        // Must be last so it renames every table and column
        builder.UseSnakeCaseNamingConvention();
    }
}
