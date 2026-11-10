using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.IAM.Application.Services;

public interface IUserCommandService
{
    Task<Result<User, CreateUserError>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default);
    Task<Result<User, UpdateUserError>> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default);
}