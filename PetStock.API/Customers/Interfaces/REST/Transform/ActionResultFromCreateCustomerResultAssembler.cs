
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 409 or 500 with a translated message.</summary>
public static class ActionResultFromCreateCustomerResultAssembler
{
    public static ActionResult ToActionResultFromCreateCustomerResult(
        Result<Customer, CreateCustomerError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getCustomerByIdActionName) =>
        result switch
        {
            Result<Customer, CreateCustomerError>.Success success =>
                controller.CreatedAtAction(
                    getCustomerByIdActionName,
                    new { id = success.Value.Id },
                    CustomerResourceFromEntityAssembler.ToResourceFromEntity(
                        success.Value)),

            Result<Customer, CreateCustomerError>.Failure failure =>
                failure.Error switch
                {
                    CreateCustomerError.DuplicateCustomerEmail =>
                        controller.Conflict(
                            localizer["CustomerEmailDuplicated"].Value),

                    CreateCustomerError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingCustomer"].Value,
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