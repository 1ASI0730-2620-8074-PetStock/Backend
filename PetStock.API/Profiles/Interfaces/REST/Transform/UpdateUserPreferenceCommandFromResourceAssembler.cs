using PetStock.API.Profiles.Domain.Model.Commands;
using PetStock.API.Profiles.Interfaces.REST.Resources;

namespace PetStock.API.Profiles.Interfaces.REST.Transform;

/// <summary>Converts the request body and the route id into an update command.</summary>
public static class UpdateUserPreferenceCommandFromResourceAssembler
{
    public static UpdateUserPreferenceCommand ToCommandFromResource(int id, UpdateUserPreferenceResource resource) =>
        new(id, resource.NotificationsEnabled, resource.DarkMode, resource.Language);
}
