using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Profiles.Application.Errors;
using PetStock.API.Profiles.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Profiles.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 409 or 500 with a translated message.</summary>
public static class ActionResultFromCreateUserPreferenceResultAssembler
{
    public static ActionResult ToActionResultFromCreateUserPreferenceResult(
        Result<UserPreference, CreateUserPreferenceError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getUserPreferenceByIdActionName) =>
        result switch
        {
            Result<UserPreference, CreateUserPreferenceError>.Success success =>
                controller.CreatedAtAction(getUserPreferenceByIdActionName, new { id = success.Value.Id },
                    UserPreferenceResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<UserPreference, CreateUserPreferenceError>.Failure
                { Error: CreateUserPreferenceError.DuplicateUserPreference } =>
                controller.Conflict(localizer["UserPreferenceDuplicated"].Value),

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorSavingUserPreference"].Value,
                statusCode: 500)
        };
}
