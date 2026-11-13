using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Purchasing.Domain.Repositories;

/// <summary>Repository for purchase orders.</summary>
public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
{
    Task<IEnumerable<PurchaseOrder>> ListWithItemsAsync(
        CancellationToken cancellationToken = default);

    Task<PurchaseOrder?> FindByIdWithItemsAsync(
        int id,
        CancellationToken cancellationToken = default);
}