using Microsoft.EntityFrameworkCore;
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

        builder.UseSnakeCaseNamingConvention();
    }
}