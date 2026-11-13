
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Resources;
using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400 or 500 with a translated message.</summary>
public static class ActionResultFromCreateSaleResultAssembler
{
    public static ActionResult ToActionResultFromCreateSaleResult(
        Result<Sale, CreateSaleError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getSaleByIdActionName) =>
        result switch
        {
            Result<Sale, CreateSaleError>.Success success =>
                controller.CreatedAtAction(
                    getSaleByIdActionName,
                    new { id = success.Value.Id },
                    SaleResourceFromEntityAssembler.ToResourceFromEntity(
                        success.Value)),

            Result<Sale, CreateSaleError>.Failure failure =>
                failure.Error switch
                {
                    CreateSaleError.InvalidSaleItems =>
                        controller.BadRequest(
                            localizer["InvalidSaleRequest"].Value),

                    CreateSaleError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingSale"].Value,
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
