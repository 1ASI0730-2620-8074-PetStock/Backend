namespace PetStock.API.Profiles.Interfaces.REST.Resources;

/// <summary>User preferences returned by the API.</summary>
public record UserPreferenceResource(
    int Id,
    int UserId,
    bool NotificationsEnabled,
    bool DarkMode,
    string Language);
