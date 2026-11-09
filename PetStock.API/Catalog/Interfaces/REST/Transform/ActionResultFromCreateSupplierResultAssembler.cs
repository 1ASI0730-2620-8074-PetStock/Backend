using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400 or 500 with a translated message.</summary>
public static class ActionResultFromCreateSupplierResultAssembler
{
    public static ActionResult ToActionResultFromCreateSupplierResult(
        Result<Supplier, CreateSupplierError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getSupplierByIdActionName) =>
        result switch
        {
            Result<Supplier, CreateSupplierError>.Success success =>
                controller.CreatedAtAction(getSupplierByIdActionName, new { id = success.Value.Id },
                    SupplierResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Supplier, CreateSupplierError>.Failure failure => failure.Error switch
            {
                CreateSupplierError.InvalidSupplierData =>
                    controller.BadRequest(localizer["InvalidSupplierRequest"].Value),
                CreateSupplierError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorCreatingSupplier"].Value,
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