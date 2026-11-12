using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Queries;
using PetStock.API.Sales.Domain.Repositories;

namespace PetStock.API.Sales.Application.Internal.QueryServices;

/// <summary>Reads sales with their items from the repository.</summary>
public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> Handle(
        GetAllSalesQuery query,
        CancellationToken cancellationToken = default) =>
        await saleRepository.ListWithItemsAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Sale?> Handle(
        GetSaleByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await saleRepository.FindByIdWithItemsAsync(
            query.Id,
            cancellationToken);
}