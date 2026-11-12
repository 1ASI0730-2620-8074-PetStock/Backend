using Microsoft.EntityFrameworkCore;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Sales.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for sales. Loads the items of each sale.</summary>
public class SaleRepository(AppDbContext context)
    : BaseRepository<Sale>(context), ISaleRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> ListWithItemsAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sale>()
            .Include(s => s.Items)
            .OrderByDescending(s => s.SaleDate)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Sale?> FindByIdWithItemsAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sale>()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(
                s => s.Id == id,
                cancellationToken);
    }
}