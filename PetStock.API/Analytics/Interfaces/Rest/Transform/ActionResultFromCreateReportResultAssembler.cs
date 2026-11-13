using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Analytics.Application.Errors;
using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Interfaces.REST.Resources;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;
namespace PetStock.API.Analytics.Interfaces.REST.Transform;

/// <summary>
/// Converts the result of creating a report into an HTTP action result.
/// </summary>
public static class ActionResultFromCreateReportResultAssembler
{
    public static ActionResult
        ToActionResultFromCreateReportResult(
            Result<Report, CreateReportError> result,
            ControllerBase controller,
            IStringLocalizer<SharedResource> localizer,
            string getReportByIdActionName)
    {
        return result switch
        {
            Result<
                Report,
                CreateReportError>.Success success =>
                controller.CreatedAtAction(
                    getReportByIdActionName,
                    new { id = success.Value.Id },
                    ReportResourceFromEntityAssembler
                        .ToResourceFromEntity(success.Value)),

            Result<
                Report,
                CreateReportError>.Failure failure =>
                failure.Error switch
                {
                    CreateReportError.InvalidReportData =>
                        controller.BadRequest(
                            localizer["InvalidReportRequest"].Value),

                    CreateReportError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingReport"].Value,
                            statusCode:
                                StatusCodes.Status500InternalServerError),

                    _ =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                            statusCode:
                                StatusCodes.Status500InternalServerError)
                }, 
            _ =>
                controller.Problem(
                    title: localizer["UnexpectedServerError"].Value,
                    detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                    statusCode:
                        StatusCodes.Status500InternalServerError)
        };
    }
}