using Microsoft.EntityFrameworkCore;
using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.IAM.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.IAM.Application.Internal.CommandServices;

public class SessionCommandService(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    ILogger<SessionCommandService> logger)
    : ISessionCommandService
{
    public async Task<Result<Session, SignInError>> Handle(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FindByEmailAsync(command.Email.Trim().ToLower(), cancellationToken);
        if (user is null || user.Password != command.Password)
        {
            logger.LogWarning("Invalid sign in for email {Email}", command.Email);
            return new Result<Session, SignInError>.Failure(SignInError.InvalidCredentials);
        }

        try
        {
            var createSessionCommand = new CreateSessionCommand(
                user.Id,
                Guid.NewGuid().ToString(),
                DateTimeOffset.UtcNow.AddHours(8));
            var session = new Session(createSessionCommand);
            await sessionRepository.AddAsync(session, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Session, SignInError>.Success(session);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed opening a session for user {UserId}", user.Id);
            return new Result<Session, SignInError>.Failure(SignInError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error opening a session for user {UserId}", user.Id);
            return new Result<Session, SignInError>.Failure(SignInError.UnexpectedError);
        }
    }
}