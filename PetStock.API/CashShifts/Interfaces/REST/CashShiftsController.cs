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
[Tags("Cash Shifts")]
public class CashShiftsController(
    ICashShiftCommandService cashShiftCommandService,
    ICashShiftQueryService cashShiftQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CashShiftResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCashShift([FromBody] CreateCashShiftResource resource, CancellationToken cancellationToken)
    {
        var createCashShiftCommand = CreateCashShiftCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await cashShiftCommandService.Handle(createCashShiftCommand, cancellationToken);
        return ActionResultFromCreateCashShiftResultAssembler.ToActionResultFromCreateCashShiftResult(result, this, localizer, nameof(GetCashShiftById));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CashShiftResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCashShifts(CancellationToken cancellationToken = default)
    {
        var getAllCashShiftsQuery = new GetAllCashShiftsQuery();
        var cashShifts = await cashShiftQueryService.Handle(getAllCashShiftsQuery, cancellationToken);
        var resources = cashShifts.Select(CashShiftResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CashShiftResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCashShiftById(int id, CancellationToken cancellationToken = default)
    {
        var getCashShiftByIdQuery = new GetCashShiftByIdQuery(id);
        var cashShift = await cashShiftQueryService.Handle(getCashShiftByIdQuery, cancellationToken);
        if (cashShift is null) return NotFound();
        var resource = CashShiftResourceFromEntityAssembler.ToResourceFromEntity(cashShift);
        return Ok(resource);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CashShiftResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CloseCashShift(int id, [FromBody] CloseCashShiftResource resource, CancellationToken cancellationToken)
    {
        var closeCashShiftCommand = CloseCashShiftCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await cashShiftCommandService.Handle(closeCashShiftCommand, cancellationToken);
        return ActionResultFromCloseCashShiftResultAssembler.ToActionResultFromCloseCashShiftResult(result, this, localizer);
    }
}