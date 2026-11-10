using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.IAM.Application.Errors;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class ActionResultFromUpdateUserResultAssembler
{
    public static ActionResult ToActionResultFromUpdateUserResult(
        Result<User, UpdateUserError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<User, UpdateUserError>.Success success =>
                controller.Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<User, UpdateUserError>.Failure failure => failure.Error switch
            {
                UpdateUserError.UserNotFound => controller.NotFound(localizer["UserNotFound"].Value),
                UpdateUserError.DuplicateUserEmail => controller.Conflict(localizer["UserEmailDuplicated"].Value),
                UpdateUserError.InvalidUserData => controller.BadRequest(localizer["UserRequestInvalid"].Value),
                UpdateUserError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorUpdatingUser"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}