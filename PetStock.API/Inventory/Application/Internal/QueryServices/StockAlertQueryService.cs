using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Queries;
using PetStock.API.Inventory.Domain.Repositories;

namespace PetStock.API.Inventory.Application.Internal.QueryServices;

/// <summary>Reads stock alerts from the repository.</summary>
public class StockAlertQueryService(IStockAlertRepository stockAlertRepository) : IStockAlertQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<StockAlert>> Handle(GetAllStockAlertsQuery query,
        CancellationToken cancellationToken = default) =>
        await stockAlertRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<StockAlert?> Handle(GetStockAlertByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await stockAlertRepository.FindByIdAsync(query.Id, cancellationToken);
}