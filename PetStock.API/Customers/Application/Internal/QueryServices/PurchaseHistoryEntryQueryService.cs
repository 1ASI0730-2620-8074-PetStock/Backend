using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Queries;
using PetStock.API.Customers.Domain.Repositories;

namespace PetStock.API.Customers.Application.Internal.QueryServices;

/// <summary>Reads purchase history entries from the repository.</summary>
public class PurchaseHistoryEntryQueryService(
    IPurchaseHistoryEntryRepository purchaseHistoryEntryRepository)
    : IPurchaseHistoryEntryQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<PurchaseHistoryEntry>> Handle(
        GetAllPurchaseHistoryEntriesQuery query,
        CancellationToken cancellationToken = default) =>
        await purchaseHistoryEntryRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<PurchaseHistoryEntry?> Handle(
        GetPurchaseHistoryEntryByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await purchaseHistoryEntryRepository.FindByIdAsync(
            query.Id,
            cancellationToken);
}