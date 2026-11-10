using Microsoft.EntityFrameworkCore;
using PetStock.API.Profiles.Application.Errors;
using PetStock.API.Profiles.Application.Services;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Profiles.Domain.Model.Commands;
using PetStock.API.Profiles.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Profiles.Application.Internal.CommandServices;

/// <summary>Creates and updates user preferences. A user can only have one record.</summary>
public class UserPreferenceCommandService(
    IUserPreferenceRepository userPreferenceRepository,
    IUnitOfWork unitOfWork,
    ILogger<UserPreferenceCommandService> logger)
    : IUserPreferenceCommandService
{
    /// <inheritdoc />
    public async Task<Result<UserPreference, CreateUserPreferenceError>> Handle(
        CreateUserPreferenceCommand command, CancellationToken cancellationToken = default)
    {
        // Only one preference record per user
        if (await userPreferenceRepository.ExistsByUserIdAsync(command.UserId, cancellationToken))
        {
            logger.LogWarning("Duplicate preferences rejected for user {UserId}", command.UserId);
            return new Result<UserPreference, CreateUserPreferenceError>.Failure(
                CreateUserPreferenceError.DuplicateUserPreference);
        }

        try
        {
            var userPreference = new UserPreference(command);
            await userPreferenceRepository.AddAsync(userPreference, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<UserPreference, CreateUserPreferenceError>.Success(userPreference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating preferences for user {UserId}", command.UserId);
            return new Result<UserPreference, CreateUserPreferenceError>.Failure(
                CreateUserPreferenceError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<UserPreference, UpdateUserPreferenceError>> Handle(
        UpdateUserPreferenceCommand command, CancellationToken cancellationToken = default)
    {
        var userPreference = await userPreferenceRepository.FindByIdAsync(command.Id, cancellationToken);
        if (userPreference is null)
            return new Result<UserPreference, UpdateUserPreferenceError>.Failure(
                UpdateUserPreferenceError.UserPreferenceNotFound);

        try
        {
            userPreference.Update(command);
            userPreferenceRepository.Update(userPreference);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<UserPreference, UpdateUserPreferenceError>.Success(userPreference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed updating preferences {UserPreferenceId}", command.Id);
            return new Result<UserPreference, UpdateUserPreferenceError>.Failure(
                UpdateUserPreferenceError.UnexpectedError);
        }
    }
}
