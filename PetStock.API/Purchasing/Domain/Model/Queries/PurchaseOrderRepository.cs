using Microsoft.EntityFrameworkCore;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Purchasing.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for purchase orders.</summary>
public class PurchaseOrderRepository(AppDbContext context)
    : BaseRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{
    public async Task<IEnumerable<PurchaseOrder>> ListWithItemsAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<PurchaseOrder>()
            .Include(o => o.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task<PurchaseOrder?> FindByIdWithItemsAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<PurchaseOrder>()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}