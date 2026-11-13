using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Analytics.Application.Services;
using PetStock.API.Analytics.Domain.Model.Queries;
using PetStock.API.Analytics.Interfaces.REST.Resources;
using PetStock.API.Analytics.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Analytics.Interfaces.REST;

/// <summary>
/// Endpoints for operational reports.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Reports")]
public class ReportsController(
    IReportCommandService reportCommandService,
    IReportQueryService reportQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// Creates a report record.
    [HttpPost]
    [ProducesResponseType(
        typeof(ReportResource),
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateReport(
        [FromBody] CreateReportResource resource,
        CancellationToken cancellationToken = default)
    {
        var command =
            CreateReportCommandFromResourceAssembler
                .ToCommandFromResource(resource);

        var result =
            await reportCommandService.Handle(
                command,
                cancellationToken);

        return ActionResultFromCreateReportResultAssembler
            .ToActionResultFromCreateReportResult(
                result,
                this,
                localizer,
                nameof(GetReportById));
    }
    /// Gets all reports.
    [HttpGet]
    [ProducesResponseType(
        typeof(IEnumerable<ReportResource>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllReports(
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllReportsQuery();

        var reports =
            await reportQueryService.Handle(
                query,
                cancellationToken);

        var resources =
            reports.Select(
                ReportResourceFromEntityAssembler
                    .ToResourceFromEntity);

        return Ok(resources);
    }
    
    /// we get a report by id.

    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(ReportResource),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetReportById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var query =
            new GetReportByIdQuery(id);

        var report =
            await reportQueryService.Handle(
                query,
                cancellationToken);

        if (report is null)
            return NotFound();

        var resource =
            ReportResourceFromEntityAssembler
                .ToResourceFromEntity(report);

        return Ok(resource);
    }
}