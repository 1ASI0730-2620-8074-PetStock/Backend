using PetStock.API.Profiles.Domain.Model.Commands;

namespace PetStock.API.Profiles.Domain.Model.Aggregates;

/// <summary>Configuration preferences of a user (one per user).</summary>
public partial class UserPreference
{
    // Constructor for EF Core
    protected UserPreference()
    {
        Language = string.Empty;
    }

    /// <summary>Creates the preferences from the command.</summary>
    public UserPreference(CreateUserPreferenceCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        UserId = command.UserId;
        NotificationsEnabled = command.NotificationsEnabled;
        DarkModeEnabled = command.DarkModeEnabled;
        Language = command.Language;
    }

    public int Id { get; private set; }

    /// <summary>User id from the IAM bounded context.</summary>
    public int UserId { get; private set; }

    public bool NotificationsEnabled { get; private set; }
    public bool DarkModeEnabled { get; private set; }

    /// <summary>Language code: en or es.</summary>
    public string Language { get; private set; }
}
