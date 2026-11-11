using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Queries;
using PetStock.API.Purchasing.Domain.Repositories;

namespace PetStock.API.Purchasing.Application.Internal.QueryServices;

/// <summary>
/// Handles queries related to purchase orders.
/// </summary>
public class PurchaseOrderQueryService(
    IPurchaseOrderRepository purchaseOrderRepository)
    : IPurchaseOrderQueryService
{
    /// <summary>
    /// Gets all purchase orders including their items.
    /// </summary>
    public async Task<IEnumerable<PurchaseOrder>>
        Handle(
            GetAllPurchaseOrdersQuery query,
            CancellationToken cancellationToken = default)
    {
        return await purchaseOrderRepository
            .ListWithItemsAsync(
                cancellationToken);
    }

    /// <summary>
    /// Gets one purchase order including its items.
    /// </summary>
    public async Task<PurchaseOrder?>
        Handle(
            GetPurchaseOrderByIdQuery query,
            CancellationToken cancellationToken = default)
    {
        return await purchaseOrderRepository
            .FindByIdWithItemsAsync(
                query.Id,
                cancellationToken);
    }
}