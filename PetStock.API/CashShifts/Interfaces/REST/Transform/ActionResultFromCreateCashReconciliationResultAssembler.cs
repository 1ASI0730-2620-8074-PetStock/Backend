using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class ActionResultFromCreateCashReconciliationResultAssembler
{
    public static ActionResult ToActionResultFromCreateCashReconciliationResult(
        Result<CashReconciliation, CreateCashReconciliationError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getCashReconciliationByIdActionName) =>
        result switch
        {
            Result<CashReconciliation, CreateCashReconciliationError>.Success success =>
                controller.CreatedAtAction(getCashReconciliationByIdActionName, new { id = success.Value.Id }, CashReconciliationResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<CashReconciliation, CreateCashReconciliationError>.Failure failure => failure.Error switch
            {
                CreateCashReconciliationError.CashShiftNotFound => controller.NotFound(localizer["CashShiftNotFound"].Value),
                CreateCashReconciliationError.DuplicateCashReconciliation => controller.Conflict(localizer["CashReconciliationDuplicated"].Value),
                CreateCashReconciliationError.UnexpectedError => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorCreatingCashReconciliation"].Value, statusCode: 500),
                _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
            },
            _ => controller.Problem(title: localizer["UnexpectedServerError"].Value, detail: localizer["UnexpectedErrorProcessingRequest"].Value, statusCode: 500)
        };
}