using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Queries;

namespace PetStock.API.Sales.Application.Services;

/// <summary>Read operations for sales.</summary>
public interface ISaleQueryService
{
    /// <summary>Lists all sales with their items.</summary>
    Task<IEnumerable<Sale>> Handle(
        GetAllSalesQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one sale with its items or null.</summary>
    Task<Sale?> Handle(
        GetSaleByIdQuery query,
        CancellationToken cancellationToken = default);
}