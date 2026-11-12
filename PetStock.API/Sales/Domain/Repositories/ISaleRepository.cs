using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Sales.Domain.Repositories;

/// <summary>Repository for sales.</summary>
public interface ISaleRepository : IBaseRepository<Sale>
{
    /// <summary>Lists all sales with their items, newest first.</summary>
    Task<IEnumerable<Sale>> ListWithItemsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Gets one sale with its items, or null if it does not exist.</summary>
    Task<Sale?> FindByIdWithItemsAsync(
        int id,
        CancellationToken cancellationToken = default);
}