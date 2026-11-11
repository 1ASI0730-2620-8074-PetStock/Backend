using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Domain.Model.Queries;
using PetStock.API.Purchasing.Interfaces.REST.Resources;
using PetStock.API.Purchasing.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST;

/// <summary>Endpoints for purchases made to suppliers.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Purchases")]
public class PurchasesController(
    IPurchaseOrderCommandService purchaseOrderCommandService,
    IPurchaseOrderQueryService purchaseOrderQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Registers a purchase of one product to a supplier.</summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(PurchaseResource),
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreatePurchase(
        [FromBody] CreatePurchaseResource resource,
        CancellationToken cancellationToken)
    {
        var createPurchaseOrderCommand =
            CreatePurchaseOrderCommandFromResourceAssembler
                .ToCommandFromResource(resource);

        var result =
            await purchaseOrderCommandService.Handle(
                createPurchaseOrderCommand,
                cancellationToken);

        return ActionResultFromCreatePurchaseResultAssembler
            .ToActionResultFromCreatePurchaseResult(
                result,
                this,
                localizer,
                nameof(GetPurchaseById));
    }

    /// <summary>Lists all purchases.</summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IEnumerable<PurchaseResource>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPurchases(
        CancellationToken cancellationToken = default)
    {
        var getAllPurchaseOrdersQuery =
            new GetAllPurchaseOrdersQuery();

        var purchaseOrders =
            await purchaseOrderQueryService.Handle(
                getAllPurchaseOrdersQuery,
                cancellationToken);

        var resources =
            purchaseOrders.Select(
                PurchaseResourceFromEntityAssembler
                    .ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>Gets one purchase by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(PurchaseResource),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPurchaseById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getPurchaseOrderByIdQuery =
            new GetPurchaseOrderByIdQuery(id);

        var purchaseOrder =
            await purchaseOrderQueryService.Handle(
                getPurchaseOrderByIdQuery,
                cancellationToken);

        if (purchaseOrder is null)
            return NotFound();

        var resource =
            PurchaseResourceFromEntityAssembler
                .ToResourceFromEntity(purchaseOrder);

        return Ok(resource);
    }

    /// <summary>Deletes one purchase by id.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(
        StatusCodes.Status204NoContent)]
    [ProducesResponseType(
        typeof(string),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePurchase(
        int id,
        CancellationToken cancellationToken = default)
    {
        var deletePurchaseOrderCommand =
            new DeletePurchaseOrderCommand(id);

        var deleted =
            await purchaseOrderCommandService.Handle(
                deletePurchaseOrderCommand,
                cancellationToken);

        if (!deleted)
            return NotFound(
                localizer["PurchaseNotFound"].Value);

        return NoContent();
    }
}