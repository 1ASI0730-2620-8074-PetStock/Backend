using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Inventory.Application.Services;

/// <summary>Write operations for stock alerts.</summary>
public interface IStockAlertCommandService
{
    /// <summary>Creates a stock alert; returns the alert or the error.</summary>
    Task<Result<StockAlert, CreateStockAlertError>> Handle(CreateStockAlertCommand command,
        CancellationToken cancellationToken = default);
}