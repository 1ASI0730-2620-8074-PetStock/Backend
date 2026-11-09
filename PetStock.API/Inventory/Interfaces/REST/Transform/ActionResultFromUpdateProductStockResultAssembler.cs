using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Turns the update result into 200, 400, 404 or 500 with a translated message.</summary>
public static class ActionResultFromUpdateProductStockResultAssembler
{
    public static ActionResult ToActionResultFromUpdateProductStockResult(
        Result<ProductStock, UpdateProductStockError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<ProductStock, UpdateProductStockError>.Success success =>
                controller.Ok(ProductStockResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<ProductStock, UpdateProductStockError>.Failure failure => failure.Error switch
            {
                UpdateProductStockError.ProductStockNotFound =>
                    controller.NotFound(localizer["ProductStockNotFound"].Value),

                UpdateProductStockError.InvalidStockQuantities =>
                    controller.BadRequest(localizer["InvalidProductStockRequest"].Value),

                UpdateProductStockError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorUpdatingProductStock"].Value,
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