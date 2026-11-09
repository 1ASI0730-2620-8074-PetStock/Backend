using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Queries;

namespace PetStock.API.Inventory.Application.Services;

/// <summary>Read operations for product stocks.</summary>
public interface IProductStockQueryService
{
    /// <summary>Lists the stock of all products.</summary>
    Task<IEnumerable<ProductStock>> Handle(GetAllProductStocksQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one product stock or null.</summary>
    Task<ProductStock?> Handle(GetProductStockByIdQuery query, CancellationToken cancellationToken = default);
}