
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 404 or 500 with a translated message.</summary>
public static class ActionResultFromCreatePurchaseHistoryEntryResultAssembler
{
    public static ActionResult ToActionResultFromCreatePurchaseHistoryEntryResult(
        Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getPurchaseHistoryEntryByIdActionName) =>
        result switch
        {
            Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>.Success success =>
                controller.CreatedAtAction(
                    getPurchaseHistoryEntryByIdActionName,
                    new { id = success.Value.Id },
                    PurchaseHistoryEntryResourceFromEntityAssembler.ToResourceFromEntity(
                        success.Value)),

            Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>.Failure failure =>
                failure.Error switch
                {
                    CreatePurchaseHistoryEntryError.CustomerNotFound =>
                        controller.NotFound(
                            localizer["HistoryCustomerNotFound"].Value),

                    CreatePurchaseHistoryEntryError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingHistoryEntry"].Value,
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