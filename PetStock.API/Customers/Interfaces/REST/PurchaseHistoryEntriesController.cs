
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Queries;
using PetStock.API.Customers.Interfaces.REST.Resources;
using PetStock.API.Customers.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Customers.Interfaces.REST;

/// <summary>Endpoints for the purchase history of customers.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Purchase History")]
public class PurchaseHistoryEntriesController(
    IPurchaseHistoryEntryCommandService purchaseHistoryEntryCommandService,
    IPurchaseHistoryEntryQueryService purchaseHistoryEntryQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Saves a sale in the history of an existing customer.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(PurchaseHistoryEntryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreatePurchaseHistoryEntry(
        [FromBody] CreatePurchaseHistoryEntryResource resource,
        CancellationToken cancellationToken)
    {
        var createCommand =
            CreatePurchaseHistoryEntryCommandFromResourceAssembler.ToCommandFromResource(
                resource);

        var result = await purchaseHistoryEntryCommandService.Handle(
            createCommand,
            cancellationToken);

        return ActionResultFromCreatePurchaseHistoryEntryResultAssembler
            .ToActionResultFromCreatePurchaseHistoryEntryResult(
                result,
                this,
                localizer,
                nameof(GetPurchaseHistoryEntryById));
    }

    /// <summary>Lists all purchase history entries.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PurchaseHistoryEntryResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPurchaseHistoryEntries(
        CancellationToken cancellationToken = default)
    {
        var getAllQuery = new GetAllPurchaseHistoryEntriesQuery();

        var entries = await purchaseHistoryEntryQueryService.Handle(
            getAllQuery,
            cancellationToken);

        var resources = entries.Select(
            PurchaseHistoryEntryResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>Gets one purchase history entry by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PurchaseHistoryEntryResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPurchaseHistoryEntryById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getByIdQuery = new GetPurchaseHistoryEntryByIdQuery(id);

        var entry = await purchaseHistoryEntryQueryService.Handle(
            getByIdQuery,
            cancellationToken);

        if (entry is null)
            return NotFound();

        var resource =
            PurchaseHistoryEntryResourceFromEntityAssembler.ToResourceFromEntity(entry);

        return Ok(resource);
    }
}