using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400, 409 or 500 with a translated message.</summary>
public static class ActionResultFromCreateProductStockResultAssembler
{
    public static ActionResult ToActionResultFromCreateProductStockResult(
        Result<ProductStock, CreateProductStockError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getProductStockByIdActionName) =>
        result switch
        {
            Result<ProductStock, CreateProductStockError>.Success success =>
                controller.CreatedAtAction(getProductStockByIdActionName, new { id = success.Value.Id },
                    ProductStockResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<ProductStock, CreateProductStockError>.Failure failure => failure.Error switch
            {
                CreateProductStockError.DuplicateProductStock =>
                    controller.Conflict(localizer["ProductStockDuplicated"].Value),

                CreateProductStockError.InvalidStockQuantities =>
                    controller.BadRequest(localizer["InvalidProductStockRequest"].Value),

                CreateProductStockError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorCreatingProductStock"].Value,
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