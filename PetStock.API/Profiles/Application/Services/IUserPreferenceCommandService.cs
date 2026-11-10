using PetStock.API.Profiles.Application.Errors;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Profiles.Application.Services;

/// <summary>Write operations for user preferences.</summary>
public interface IUserPreferenceCommandService
{
    /// <summary>Creates the preferences of a user; returns them or the error.</summary>
    Task<Result<UserPreference, CreateUserPreferenceError>> Handle(CreateUserPreferenceCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>Updates the preferences; returns them or the error.</summary>
    Task<Result<UserPreference, UpdateUserPreferenceError>> Handle(UpdateUserPreferenceCommand command,
        CancellationToken cancellationToken = default);
}
