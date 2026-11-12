using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Sales.Application.Services;

/// <summary>Write operations for sales.</summary>
public interface ISaleCommandService
{
    /// <summary>Creates a sale with its items; returns the sale or the error.</summary>
    Task<Result<Sale, CreateSaleError>> Handle(
        CreateSaleCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>Deletes a sale and its items; returns false if it does not exist.</summary>
    Task<bool> Handle(
        DeleteSaleCommand command,
        CancellationToken cancellationToken = default);
}
