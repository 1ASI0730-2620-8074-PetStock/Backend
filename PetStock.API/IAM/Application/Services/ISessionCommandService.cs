using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.IAM.Application.Services;

public interface ISessionCommandService
{
    Task<Result<Session, SignInError>> Handle(SignInCommand command, CancellationToken cancellationToken = default);
}