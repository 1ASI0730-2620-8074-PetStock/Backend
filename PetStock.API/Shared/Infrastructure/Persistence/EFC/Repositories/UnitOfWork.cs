using PetStock.API.Shared.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>Unit of work implemented with EF Core SaveChangesAsync.</summary>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}