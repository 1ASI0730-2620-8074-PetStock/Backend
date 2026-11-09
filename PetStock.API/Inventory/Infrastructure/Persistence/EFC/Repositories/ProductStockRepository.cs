using Microsoft.EntityFrameworkCore;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for product stocks.</summary>
public class ProductStockRepository(AppDbContext context)
    : BaseRepository<ProductStock>(context), IProductStockRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductStock>()
            .AnyAsync(s => s.ProductId == productId, cancellationToken);
    }
}