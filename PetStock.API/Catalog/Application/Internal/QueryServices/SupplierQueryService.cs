using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.QueryServices;

/// <summary>Reads suppliers from the repository.</summary>
public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query,
        CancellationToken cancellationToken = default) =>
        await supplierRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Supplier?> Handle(GetSupplierByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await supplierRepository.FindByIdAsync(query.Id, cancellationToken);
}