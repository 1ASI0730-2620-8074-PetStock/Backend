using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Inventory.Application.Services;

/// <summary>Write operations for product stocks.</summary>
public interface IProductStockCommandService
{
    /// <summary>Registers the stock of a product; returns the stock or the error.</summary>
    Task<Result<ProductStock, CreateProductStockError>> Handle(CreateProductStockCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>Updates the stock of a product; returns the stock or the error.</summary>
    Task<Result<ProductStock, UpdateProductStockError>> Handle(UpdateProductStockCommand command,
        CancellationToken cancellationToken = default);
}