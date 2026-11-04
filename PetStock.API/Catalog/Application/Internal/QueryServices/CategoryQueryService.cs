using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.QueryServices;

/// <summary>Reads categories from the repository.</summary>
public class CategoryQueryService(ICategoryRepository categoryRepository) : ICategoryQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query,
        CancellationToken cancellationToken = default) =>
        await categoryRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Category?> Handle(GetCategoryByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await categoryRepository.FindByIdAsync(query.Id, cancellationToken);
}
