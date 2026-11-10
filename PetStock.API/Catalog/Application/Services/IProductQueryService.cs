using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Read operations for products.</summary>
public interface IProductQueryService
{
    /// <summary>Lists all products.</summary>
    Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken = default);

    /// <summary>Gets one product or null.</summary>
    Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken = default);
}