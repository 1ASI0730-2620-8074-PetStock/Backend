using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class ActionResultFromCreateCashShiftResultAssembler
{
    public static ActionResult ToActionResultFromCreateCashShiftResult(
        Result<CashShift, CreateCashShiftError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getCashShiftByIdActionName) =>
        result switch
        {
            Result<CashShift, CreateCashShiftError>.Success success =>
                controller.CreatedAtAction(getCashShiftByIdActionName, new { id = success.Value.Id }, CashShiftResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<CashShift, CreateCashShiftError>.Failure failure => failure.Error switch
            {
                CreateCashShiftError.UserAlreadyHasOpenShift => controller.Conflict(localizer["CashShiftAlreadyOpen"].Value),
                CreateCashShiftError.InvalidCashShiftData => controller.BadRequest(localizer["CashShiftRequestInvalid"].Value),
                CreateCashShiftError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorOpeningCashShift"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}