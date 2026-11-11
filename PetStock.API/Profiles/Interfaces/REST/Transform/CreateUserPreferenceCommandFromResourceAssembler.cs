using PetStock.API.Profiles.Domain.Model.Commands;
using PetStock.API.Profiles.Interfaces.REST.Resources;

namespace PetStock.API.Profiles.Interfaces.REST.Transform;

/// <summary>Converts the request body into a create command.</summary>
public static class CreateUserPreferenceCommandFromResourceAssembler
{
    public static CreateUserPreferenceCommand ToCommandFromResource(CreateUserPreferenceResource resource) =>
        new(resource.UserId, resource.NotificationsEnabled, resource.DarkMode, resource.Language);
}
