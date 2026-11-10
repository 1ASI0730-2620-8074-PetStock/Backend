using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Profiles.Interfaces.REST.Resources;

/// <summary>Body of PUT /preferences/{id}.</summary>
public record UpdateUserPreferenceResource(
    bool NotificationsEnabled,
    bool DarkMode,
    [Required(ErrorMessage = "RequiredPreferenceLanguage")]
    [RegularExpression("^(en|es)$", ErrorMessage = "InvalidPreferenceLanguage")]
    string Language);
