using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Queries;

namespace PetStock.API.Customers.Application.Services;

/// <summary>Read operations for the purchase history.</summary>
public interface IPurchaseHistoryEntryQueryService
{
    /// <summary>Lists all purchase history entries.</summary>
    Task<IEnumerable<PurchaseHistoryEntry>> Handle(
        GetAllPurchaseHistoryEntriesQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one purchase history entry or null.</summary>
    Task<PurchaseHistoryEntry?> Handle(
        GetPurchaseHistoryEntryByIdQuery query,
        CancellationToken cancellationToken = default);
}