using PetStock.API.Profiles.Application.Services;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Domain.Model.Queries;
using PetStock.API.Profiles.Domain.Repositories;

namespace PetStock.API.Profiles.Application.Internal.QueryServices;

/// <summary>Reads user preferences from the repository.</summary>
public class UserPreferenceQueryService(IUserPreferenceRepository userPreferenceRepository)
    : IUserPreferenceQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<UserPreference>> Handle(GetAllUserPreferencesQuery query,
        CancellationToken cancellationToken = default) =>
        await userPreferenceRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<UserPreference?> Handle(GetUserPreferenceByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await userPreferenceRepository.FindByIdAsync(query.Id, cancellationToken);
}
