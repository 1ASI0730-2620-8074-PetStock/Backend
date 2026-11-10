using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class ActionResultFromCreateUserResultAssembler
{
    public static ActionResult ToActionResultFromCreateUserResult(
        Result<User, CreateUserError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getUserByIdActionName) =>
        result switch
        {
            Result<User, CreateUserError>.Success success =>
                controller.CreatedAtAction(getUserByIdActionName, new { id = success.Value.Id }, UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<User, CreateUserError>.Failure failure => failure.Error switch
            {
                CreateUserError.DuplicateUserEmail => controller.Conflict(localizer["UserEmailDuplicated"].Value),
                CreateUserError.InvalidUserData => controller.BadRequest(localizer["UserRequestInvalid"].Value),
                CreateUserError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorCreatingUser"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}