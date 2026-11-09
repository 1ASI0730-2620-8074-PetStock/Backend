using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Queries;

namespace PetStock.API.Inventory.Application.Services;

/// <summary>Read operations for stock alerts.</summary>
public interface IStockAlertQueryService
{
    /// <summary>Lists all stock alerts.</summary>
    Task<IEnumerable<StockAlert>> Handle(GetAllStockAlertsQuery query, CancellationToken cancellationToken = default);

    /// <summary>Gets one stock alert or null.</summary>
    Task<StockAlert?> Handle(GetStockAlertByIdQuery query, CancellationToken cancellationToken = default);
}