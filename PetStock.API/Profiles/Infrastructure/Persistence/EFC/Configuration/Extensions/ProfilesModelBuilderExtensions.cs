using Microsoft.EntityFrameworkCore;
using PetStock.API.Profiles.Domain.Model.Aggregates;

namespace PetStock.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the Profiles bounded context.</summary>
public static class ProfilesModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // user_preferences (one per user)
        builder.Entity<UserPreference>().HasKey(p => p.Id);
        builder.Entity<UserPreference>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UserPreference>().Property(p => p.UserId).IsRequired();
        builder.Entity<UserPreference>().Property(p => p.NotificationsEnabled).IsRequired();
        builder.Entity<UserPreference>().Property(p => p.DarkModeEnabled).IsRequired();
        builder.Entity<UserPreference>().Property(p => p.Language).IsRequired().HasMaxLength(10);
        builder.Entity<UserPreference>().HasIndex(p => p.UserId).IsUnique();
    }
}
