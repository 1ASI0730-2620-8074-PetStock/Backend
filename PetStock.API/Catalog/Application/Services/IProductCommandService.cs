using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Write operations for products.</summary>
public interface IProductCommandService
{
    /// <summary>Creates a product; returns the product or the error.</summary>
    Task<Result<Product, CreateProductError>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken = default);
}