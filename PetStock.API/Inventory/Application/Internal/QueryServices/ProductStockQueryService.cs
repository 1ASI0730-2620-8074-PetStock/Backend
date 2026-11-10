using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Queries;
using PetStock.API.Inventory.Domain.Repositories;

namespace PetStock.API.Inventory.Application.Internal.QueryServices;

/// <summary>Reads product stocks from the repository.</summary>
public class ProductStockQueryService(IProductStockRepository productStockRepository) : IProductStockQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ProductStock>> Handle(GetAllProductStocksQuery query,
        CancellationToken cancellationToken = default) =>
        await productStockRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<ProductStock?> Handle(GetProductStockByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await productStockRepository.FindByIdAsync(query.Id, cancellationToken);
}