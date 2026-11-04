namespace PetStock.API.Profiles.Domain.Model.Commands;

/// <summary>Data needed to create the preferences of a user.</summary>
public record CreateUserPreferenceCommand(
    int UserId,
    bool NotificationsEnabled,
    bool DarkModeEnabled,
    string Language);
