using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Model.Queries;
using PetStock.API.CashShifts.Interfaces.REST.Resources;
using PetStock.API.CashShifts.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Cash Reconciliations")]
public class CashReconciliationsController(
    ICashReconciliationCommandService cashReconciliationCommandService,
    ICashReconciliationQueryService cashReconciliationQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CashReconciliationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCashReconciliation([FromBody] CreateCashReconciliationResource resource, CancellationToken cancellationToken)
    {
        var createCashReconciliationCommand = CreateCashReconciliationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await cashReconciliationCommandService.Handle(createCashReconciliationCommand, cancellationToken);
        return ActionResultFromCreateCashReconciliationResultAssembler.ToActionResultFromCreateCashReconciliationResult(result, this, localizer, nameof(GetCashReconciliationById));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CashReconciliationResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCashReconciliations(CancellationToken cancellationToken = default)
    {
        var getAllCashReconciliationsQuery = new GetAllCashReconciliationsQuery();
        var cashReconciliations = await cashReconciliationQueryService.Handle(getAllCashReconciliationsQuery, cancellationToken);
        var resources = cashReconciliations.Select(CashReconciliationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CashReconciliationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCashReconciliationById(int id, CancellationToken cancellationToken = default)
    {
        var getCashReconciliationByIdQuery = new GetCashReconciliationByIdQuery(id);
        var cashReconciliation = await cashReconciliationQueryService.Handle(getCashReconciliationByIdQuery, cancellationToken);
        if (cashReconciliation is null) return NotFound();
        var resource = CashReconciliationResourceFromEntityAssembler.ToResourceFromEntity(cashReconciliation);
        return Ok(resource);
    }
}