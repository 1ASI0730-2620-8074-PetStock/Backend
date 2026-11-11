using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Interfaces.REST.Resources;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Converts the result of creating a purchase into an HTTP action result.
/// </summary>
public static class ActionResultFromCreatePurchaseResultAssembler
{
    public static ActionResult ToActionResultFromCreatePurchaseResult(
        Result<PurchaseOrder, CreatePurchaseOrderError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getPurchaseByIdActionName)
    {
        return result switch
        {
            Result<
                PurchaseOrder,
                CreatePurchaseOrderError>.Success success =>
                controller.CreatedAtAction(
                    getPurchaseByIdActionName,
                    new { id = success.Value.Id },
                    PurchaseResourceFromEntityAssembler
                        .ToResourceFromEntity(success.Value)),

            Result<
                PurchaseOrder,
                CreatePurchaseOrderError>.Failure failure =>
                failure.Error switch
                {
                    CreatePurchaseOrderError.InvalidPurchaseData =>
                        controller.BadRequest(
                            localizer[
                                "InvalidPurchaseRequest"].Value),

                    CreatePurchaseOrderError.UnexpectedError =>
                        controller.Problem(
                            title: localizer[
                                "UnexpectedServerError"].Value,
                            detail: localizer[
                                "UnexpectedErrorCreatingPurchase"].Value,
                            statusCode:
                                StatusCodes.Status500InternalServerError),

                    _ =>
                        controller.Problem(
                            title: localizer[
                                "UnexpectedServerError"].Value,
                            detail: localizer[
                                "UnexpectedErrorProcessingRequest"].Value,
                            statusCode:
                                StatusCodes.Status500InternalServerError)
                },

            _ =>
                controller.Problem(
                    title: localizer[
                        "UnexpectedServerError"].Value,
                    detail: localizer[
                        "UnexpectedErrorProcessingRequest"].Value,
                    statusCode:
                        StatusCodes.Status500InternalServerError)
        };
    }
}