namespace PetStock.API.Profiles.Domain.Model.Commands;

/// <summary>Data needed to change the preferences of a user.</summary>
public record UpdateUserPreferenceCommand(
    int Id,
    bool NotificationsEnabled,
    bool DarkModeEnabled,
    string Language);
