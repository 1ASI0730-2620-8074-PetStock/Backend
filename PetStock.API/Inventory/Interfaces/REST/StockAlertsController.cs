using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Queries;
using PetStock.API.Inventory.Interfaces.REST.Resources;
using PetStock.API.Inventory.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Inventory.Interfaces.REST;

/// <summary>Endpoints for stock alerts.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Stock Alerts")]
public class StockAlertsController(
    IStockAlertCommandService stockAlertCommandService,
    IStockAlertQueryService stockAlertQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates an active stock alert for a product.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(StockAlertResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateStockAlert([FromBody] CreateStockAlertResource resource,
        CancellationToken cancellationToken)
    {
        var createStockAlertCommand = CreateStockAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await stockAlertCommandService.Handle(createStockAlertCommand, cancellationToken);
        return ActionResultFromCreateStockAlertResultAssembler.ToActionResultFromCreateStockAlertResult(
            result,
            this,
            localizer,
            nameof(GetStockAlertById));
    }

    /// <summary>Lists all stock alerts.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StockAlertResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllStockAlerts(CancellationToken cancellationToken = default)
    {
        var getAllStockAlertsQuery = new GetAllStockAlertsQuery();
        var stockAlerts = await stockAlertQueryService.Handle(getAllStockAlertsQuery, cancellationToken);
        var resources = stockAlerts.Select(StockAlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one stock alert by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StockAlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetStockAlertById(int id, CancellationToken cancellationToken = default)
    {
        var getStockAlertByIdQuery = new GetStockAlertByIdQuery(id);
        var stockAlert = await stockAlertQueryService.Handle(getStockAlertByIdQuery, cancellationToken);
        if (stockAlert is null) return NotFound();
        var resource = StockAlertResourceFromEntityAssembler.ToResourceFromEntity(stockAlert);
        return Ok(resource);
    }
}