using Microsoft.EntityFrameworkCore;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Catalog.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for categories.</summary>
public class CategoryRepository(AppDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Category>()
            .AnyAsync(c => c.Name == name, cancellationToken);
    }
}
