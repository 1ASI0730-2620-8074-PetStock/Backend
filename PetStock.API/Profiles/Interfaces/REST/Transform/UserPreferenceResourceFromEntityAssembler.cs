using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Interfaces.REST.Resources;

namespace PetStock.API.Profiles.Interfaces.REST.Transform;

/// <summary>Converts a UserPreference into the resource returned by the API.</summary>
public static class UserPreferenceResourceFromEntityAssembler
{
    public static UserPreferenceResource ToResourceFromEntity(UserPreference entity) =>
        new(entity.Id, entity.UserId, entity.NotificationsEnabled, entity.DarkModeEnabled, entity.Language);
}
