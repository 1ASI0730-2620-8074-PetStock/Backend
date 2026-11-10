using Microsoft.EntityFrameworkCore;
using PetStock.API.IAM.Domain.Model.Aggregates;

namespace PetStock.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Table configuration of the IAM bounded context.</summary>
public static class IamModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // users
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(u => u.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(40);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // sessions (FK to users)
        builder.Entity<Session>().HasKey(s => s.Id);
        builder.Entity<Session>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Session>().Property(s => s.Token).IsRequired().HasMaxLength(255);
        builder.Entity<Session>().Property(s => s.ExpiresAt).IsRequired();
        builder.Entity<Session>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .IsRequired();
    }
}
