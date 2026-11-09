using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Write operations for suppliers.</summary>
public interface ISupplierCommandService
{
    /// <summary>Creates a supplier; returns the supplier or the error.</summary>
    Task<Result<Supplier, CreateSupplierError>> Handle(CreateSupplierCommand command,
        CancellationToken cancellationToken = default);
}