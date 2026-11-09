using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Read operations for suppliers.</summary>
public interface ISupplierQueryService
{
    /// <summary>Lists all suppliers.</summary>
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken = default);

    /// <summary>Gets one supplier or null.</summary>
    Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken = default);
}