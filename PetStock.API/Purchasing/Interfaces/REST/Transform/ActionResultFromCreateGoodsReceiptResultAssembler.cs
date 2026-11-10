using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Interfaces.REST.Resources;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Turns the create result into 201, 404, 409 or 500 with a translated message.
/// </summary>
public static class ActionResultFromCreateGoodsReceiptResultAssembler
{
    public static ActionResult
        ToActionResultFromCreateGoodsReceiptResult(
            Result<GoodsReceipt, CreateGoodsReceiptError> result,
            ControllerBase controller,
            IStringLocalizer<SharedResource> localizer,
            string getGoodsReceiptByIdActionName) =>
        result switch
        {
            Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Success success =>
                controller.CreatedAtAction(
                    getGoodsReceiptByIdActionName,
                    new { id = success.Value.Id },
                    GoodsReceiptResourceFromEntityAssembler
                        .ToResourceFromEntity(success.Value)),

            Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Failure failure =>
                failure.Error switch
                {
                    CreateGoodsReceiptError.PurchaseOrderNotFound =>
                        controller.NotFound(
                            localizer["PurchaseNotFound"].Value),

                    CreateGoodsReceiptError.PurchaseOrderAlreadyReceived =>
                        controller.Conflict(
                            localizer["PurchaseAlreadyReceived"].Value),

                    CreateGoodsReceiptError.UnexpectedError =>
                        controller.Problem(
                            title: localizer[
                                "UnexpectedServerError"].Value,
                            detail: localizer[
                                "UnexpectedErrorCreatingGoodsReceipt"].Value,
                            statusCode: StatusCodes.Status500InternalServerError),

                    _ => controller.Problem(
                        title: localizer[
                            "UnexpectedServerError"].Value,
                        detail: localizer[
                            "UnexpectedErrorProcessingRequest"].Value,
                        statusCode: StatusCodes.Status500InternalServerError)
                },

            _ => controller.Problem(
                title: localizer[
                    "UnexpectedServerError"].Value,
                detail: localizer[
                    "UnexpectedErrorProcessingRequest"].Value,
                statusCode: StatusCodes.Status500InternalServerError)
        };
}