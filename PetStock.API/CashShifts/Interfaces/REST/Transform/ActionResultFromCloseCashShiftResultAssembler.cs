using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class ActionResultFromCloseCashShiftResultAssembler
{
    public static ActionResult ToActionResultFromCloseCashShiftResult(
        Result<CashShift, CloseCashShiftError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<CashShift, CloseCashShiftError>.Success success =>
                controller.Ok(CashShiftResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<CashShift, CloseCashShiftError>.Failure failure => failure.Error switch
            {
                CloseCashShiftError.CashShiftNotFound => controller.NotFound(localizer["CashShiftNotFound"].Value),
                CloseCashShiftError.CashShiftAlreadyClosed => controller.Conflict(localizer["CashShiftAlreadyClosed"].Value),
                CloseCashShiftError.InvalidClosingAmount => controller.BadRequest(localizer["CashShiftClosingAmountInvalid"].Value),
                CloseCashShiftError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorClosingCashShift"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}