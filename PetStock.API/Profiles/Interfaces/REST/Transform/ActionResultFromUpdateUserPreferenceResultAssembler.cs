using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Profiles.Application.Errors;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Profiles.Interfaces.REST.Transform;

/// <summary>Turns the update result into 200, 404 or 500 with a translated message.</summary>
public static class ActionResultFromUpdateUserPreferenceResultAssembler
{
    public static ActionResult ToActionResultFromUpdateUserPreferenceResult(
        Result<UserPreference, UpdateUserPreferenceError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<UserPreference, UpdateUserPreferenceError>.Success success =>
                controller.Ok(UserPreferenceResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<UserPreference, UpdateUserPreferenceError>.Failure
                { Error: UpdateUserPreferenceError.UserPreferenceNotFound } =>
                controller.NotFound(localizer["UserPreferenceNotFound"].Value),

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorSavingUserPreference"].Value,
                statusCode: 500)
        };
}
