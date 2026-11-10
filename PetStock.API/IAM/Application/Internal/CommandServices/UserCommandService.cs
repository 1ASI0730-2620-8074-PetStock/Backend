using Microsoft.EntityFrameworkCore;
using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.IAM.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UserCommandService> logger)
    : IUserCommandService
{
    public async Task<Result<User, CreateUserError>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var existingUser = await userRepository.FindByEmailAsync(command.Email.Trim().ToLower(), cancellationToken);
        if (existingUser is not null)
        {
            logger.LogWarning("Duplicate user rejected for email {Email}", command.Email);
            return new Result<User, CreateUserError>.Failure(CreateUserError.DuplicateUserEmail);
        }

        try
        {
            var user = new User(command);
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<User, CreateUserError>.Success(user);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while creating user {Email}", command.Email);
            return new Result<User, CreateUserError>.Failure(CreateUserError.InvalidUserData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating user {Email}", command.Email);
            return new Result<User, CreateUserError>.Failure(CreateUserError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating user {Email}", command.Email);
            return new Result<User, CreateUserError>.Failure(CreateUserError.UnexpectedError);
        }
    }

    public async Task<Result<User, UpdateUserError>> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user is null)
            return new Result<User, UpdateUserError>.Failure(UpdateUserError.UserNotFound);

        var userWithEmail = await userRepository.FindByEmailAsync(command.Email.Trim().ToLower(), cancellationToken);
        if (userWithEmail is not null && userWithEmail.Id != user.Id)
        {
            logger.LogWarning("Duplicate email rejected when updating user {UserId}", command.UserId);
            return new Result<User, UpdateUserError>.Failure(UpdateUserError.DuplicateUserEmail);
        }

        try
        {
            user.Update(command);
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<User, UpdateUserError>.Success(user);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while updating user {UserId}", command.UserId);
            return new Result<User, UpdateUserError>.Failure(UpdateUserError.InvalidUserData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed updating user {UserId}", command.UserId);
            return new Result<User, UpdateUserError>.Failure(UpdateUserError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error updating user {UserId}", command.UserId);
            return new Result<User, UpdateUserError>.Failure(UpdateUserError.UnexpectedError);
        }
    }
}