using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Customers.Application.Services;

/// <summary>Write operations for the purchase history.</summary>
public interface IPurchaseHistoryEntryCommandService
{
    /// <summary>Saves a purchase in the history; returns the entry or the error.</summary>
    Task<Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>> Handle(
        CreatePurchaseHistoryEntryCommand command,
        CancellationToken cancellationToken = default);
}