using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400, 409 or 500 with a translated message.</summary>
public static class ActionResultFromCreateCategoryResultAssembler
{
    public static ActionResult ToActionResultFromCreateCategoryResult(
        Result<Category, CreateCategoryError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getCategoryByIdActionName) =>
        result switch
        {
            Result<Category, CreateCategoryError>.Success success =>
                controller.CreatedAtAction(getCategoryByIdActionName, new { id = success.Value.Id },
                    CategoryResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Category, CreateCategoryError>.Failure failure => failure.Error switch
            {
                CreateCategoryError.DuplicateCategoryName =>
                    controller.Conflict(localizer["CategoryNameDuplicated"].Value),

                CreateCategoryError.InvalidCategoryName =>
                    controller.BadRequest(localizer["InvalidCategoryRequest"].Value),

                CreateCategoryError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorCreatingCategory"].Value,
                        statusCode: 500),

                _ => controller.Problem(
                    title: localizer["UnexpectedServerError"].Value,
                    detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                    statusCode: 500)
            },

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                statusCode: 500)
        };
}
