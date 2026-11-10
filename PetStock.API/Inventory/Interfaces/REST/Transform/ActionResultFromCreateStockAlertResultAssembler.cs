using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400 or 500 with a translated message.</summary>
public static class ActionResultFromCreateStockAlertResultAssembler
{
    public static ActionResult ToActionResultFromCreateStockAlertResult(
        Result<StockAlert, CreateStockAlertError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getStockAlertByIdActionName) =>
        result switch
        {
            Result<StockAlert, CreateStockAlertError>.Success success =>
                controller.CreatedAtAction(getStockAlertByIdActionName, new { id = success.Value.Id },
                    StockAlertResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<StockAlert, CreateStockAlertError>.Failure failure => failure.Error switch
            {
                CreateStockAlertError.InvalidStockAlertData =>
                    controller.BadRequest(localizer["InvalidStockAlertRequest"].Value),

                CreateStockAlertError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorCreatingStockAlert"].Value,
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