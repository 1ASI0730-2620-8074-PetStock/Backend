using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Domain.Model.Queries;

namespace PetStock.API.Profiles.Application.Services;

/// <summary>Read operations for user preferences.</summary>
public interface IUserPreferenceQueryService
{
    /// <summary>Lists all user preferences.</summary>
    Task<IEnumerable<UserPreference>> Handle(GetAllUserPreferencesQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one user preference or null.</summary>
    Task<UserPreference?> Handle(GetUserPreferenceByIdQuery query, CancellationToken cancellationToken = default);
}
