using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Profiles.Interfaces.REST.Resources;

/// <summary>Body of POST /preferences. The language must be en or es.</summary>
public record CreateUserPreferenceResource(
    [Range(1, int.MaxValue, ErrorMessage = "InvalidPreferenceUserId")]
    int UserId,
    bool NotificationsEnabled,
    bool DarkMode,
    [Required(ErrorMessage = "RequiredPreferenceLanguage")]
    [RegularExpression("^(en|es)$", ErrorMessage = "InvalidPreferenceLanguage")]
    string Language);
