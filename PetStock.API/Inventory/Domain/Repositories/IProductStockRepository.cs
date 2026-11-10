using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Inventory.Domain.Repositories;

/// <summary>Repository for product stocks.</summary>
public interface IProductStockRepository : IBaseRepository<ProductStock>
{
    /// <summary>True if the product already has its stock registered.</summary>
    Task<bool> ExistsByProductIdAsync(int productId, CancellationToken cancellationToken = default);
}