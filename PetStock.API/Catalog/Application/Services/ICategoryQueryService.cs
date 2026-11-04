using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Queries;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Read operations for categories.</summary>
public interface ICategoryQueryService
{
    /// <summary>Lists all categories.</summary>
    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken = default);

    /// <summary>Gets one category or null.</summary>
    Task<Category?> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken = default);
}
