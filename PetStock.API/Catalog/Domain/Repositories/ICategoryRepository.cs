using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Catalog.Domain.Repositories;

/// <summary>Repository for categories.</summary>
public interface ICategoryRepository : IBaseRepository<Category>
{
    /// <summary>True if a category with that name already exists.</summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
