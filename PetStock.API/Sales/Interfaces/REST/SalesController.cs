
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Resources;
using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Domain.Model.Queries;
using PetStock.API.Sales.Interfaces.REST.Resources;
using PetStock.API.Sales.Interfaces.REST.Transform;

namespace PetStock.API.Sales.Interfaces.REST;

/// <summary>Endpoints for sales.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sales")]
public class SalesController(
    ISaleCommandService saleCommandService,
    ISaleQueryService saleQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Registers a sale with its items. The total and date are set by the server.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(SaleResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateSale(
        [FromBody] CreateSaleResource resource,
        CancellationToken cancellationToken)
    {
        var createSaleCommand =
            CreateSaleCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await saleCommandService.Handle(
            createSaleCommand,
            cancellationToken);

        return ActionResultFromCreateSaleResultAssembler.ToActionResultFromCreateSaleResult(
            result,
            this,
            localizer,
            nameof(GetSaleById));
    }

    /// <summary>Lists all sales with their items, newest first.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllSales(
        CancellationToken cancellationToken = default)
    {
        var getAllSalesQuery = new GetAllSalesQuery();

        var sales = await saleQueryService.Handle(
            getAllSalesQuery,
            cancellationToken);

        var resources = sales.Select(
            SaleResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>Gets one sale with its items by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SaleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSaleById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(id);

        var sale = await saleQueryService.Handle(
            getSaleByIdQuery,
            cancellationToken);

        if (sale is null)
            return NotFound();

        var resource =
            SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);

        return Ok(resource);
    }

    /// <summary>Deletes a sale with its items and payments.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSale(
        int id,
        CancellationToken cancellationToken = default)
    {
        var deleteSaleCommand = new DeleteSaleCommand(id);

        var deleted = await saleCommandService.Handle(
            deleteSaleCommand,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
