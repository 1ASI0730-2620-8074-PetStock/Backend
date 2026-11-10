using Microsoft.EntityFrameworkCore;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for user preferences.</summary>
public class UserPreferenceRepository(AppDbContext context)
    : BaseRepository<UserPreference>(context), IUserPreferenceRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<UserPreference>()
            .AnyAsync(p => p.UserId == userId, cancellationToken);
    }
}
