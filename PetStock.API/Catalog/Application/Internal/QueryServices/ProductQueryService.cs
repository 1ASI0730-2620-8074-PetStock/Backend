using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.QueryServices;

/// <summary>Reads products from the repository.</summary>
public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query,
        CancellationToken cancellationToken = default) =>
        await productRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await productRepository.FindByIdAsync(query.Id, cancellationToken);
}