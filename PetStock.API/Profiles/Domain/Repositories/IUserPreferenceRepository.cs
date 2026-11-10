using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Profiles.Domain.Repositories;

/// <summary>Repository for user preferences.</summary>
public interface IUserPreferenceRepository : IBaseRepository<UserPreference>
{
    /// <summary>True if the user already has preferences.</summary>
    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
