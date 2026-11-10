using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class ActionResultFromSignInResultAssembler
{
    public static ActionResult ToActionResultFromSignInResult(
        Result<Session, SignInError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getSessionByIdActionName) =>
        result switch
        {
            Result<Session, SignInError>.Success success =>
                controller.CreatedAtAction(getSessionByIdActionName, new { id = success.Value.Id }, SessionResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<Session, SignInError>.Failure failure => failure.Error switch
            {
                SignInError.InvalidCredentials => controller.Unauthorized(localizer["SessionInvalidCredentials"].Value),
                SignInError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorSigningIn"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}