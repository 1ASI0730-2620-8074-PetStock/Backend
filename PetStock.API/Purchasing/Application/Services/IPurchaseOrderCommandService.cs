using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Purchasing.Application.Services;

/// <summary>
/// Handles commands related to purchase orders.
/// </summary>
public interface IPurchaseOrderCommandService
{
    Task<Result<PurchaseOrder, CreatePurchaseOrderError>>
        Handle(
            CreatePurchaseOrderCommand command,
            CancellationToken cancellationToken = default);

    Task<bool>
        Handle(
            DeletePurchaseOrderCommand command,
            CancellationToken cancellationToken = default);
}