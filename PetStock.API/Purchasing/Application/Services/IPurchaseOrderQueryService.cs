using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Queries;

namespace PetStock.API.Purchasing.Application.Services;

/// <summary>
/// Handles queries related to purchase orders.
/// </summary>
public interface IPurchaseOrderQueryService
{
    Task<IEnumerable<PurchaseOrder>>
        Handle(
            GetAllPurchaseOrdersQuery query,
            CancellationToken cancellationToken = default);

    Task<PurchaseOrder?>
        Handle(
            GetPurchaseOrderByIdQuery query,
            CancellationToken cancellationToken = default);
}