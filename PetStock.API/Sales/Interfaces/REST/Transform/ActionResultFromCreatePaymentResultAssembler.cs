
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Resources;
using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 404 or 500 with a translated message.</summary>
public static class ActionResultFromCreatePaymentResultAssembler
{
    public static ActionResult ToActionResultFromCreatePaymentResult(
        Result<Payment, CreatePaymentError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getPaymentByIdActionName) =>
        result switch
        {
            Result<Payment, CreatePaymentError>.Success success =>
                controller.CreatedAtAction(
                    getPaymentByIdActionName,
                    new { id = success.Value.Id },
                    PaymentResourceFromEntityAssembler.ToResourceFromEntity(
                        success.Value)),

            Result<Payment, CreatePaymentError>.Failure failure =>
                failure.Error switch
                {
                    CreatePaymentError.SaleNotFound =>
                        controller.NotFound(
                            localizer["PaymentSaleNotFound"].Value),

                    CreatePaymentError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingPayment"].Value,
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
