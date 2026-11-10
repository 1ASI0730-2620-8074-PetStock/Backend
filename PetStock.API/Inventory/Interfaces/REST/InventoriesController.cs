using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Queries;
using PetStock.API.Inventory.Interfaces.REST.Resources;
using PetStock.API.Inventory.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Inventory.Interfaces.REST;

/// <summary>Endpoints for the stock of each product.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Inventories")]
public class InventoriesController(
    IProductStockCommandService productStockCommandService,
    IProductStockQueryService productStockQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Registers the stock of a product. Only one record per product.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProductStockResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateProductStock([FromBody] CreateProductStockResource resource,
        CancellationToken cancellationToken)
    {
        var createProductStockCommand = CreateProductStockCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await productStockCommandService.Handle(createProductStockCommand, cancellationToken);
        return ActionResultFromCreateProductStockResultAssembler.ToActionResultFromCreateProductStockResult(
            result,
            this,
            localizer,
            nameof(GetProductStockById));
    }

    /// <summary>Lists the stock of all products.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductStockResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllProductStocks(CancellationToken cancellationToken = default)
    {
        var getAllProductStocksQuery = new GetAllProductStocksQuery();
        var productStocks = await productStockQueryService.Handle(getAllProductStocksQuery, cancellationToken);
        var resources = productStocks.Select(ProductStockResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one product stock by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductStockResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProductStockById(int id, CancellationToken cancellationToken = default)
    {
        var getProductStockByIdQuery = new GetProductStockByIdQuery(id);
        var productStock = await productStockQueryService.Handle(getProductStockByIdQuery, cancellationToken);
        if (productStock is null) return NotFound();
        var resource = ProductStockResourceFromEntityAssembler.ToResourceFromEntity(productStock);
        return Ok(resource);
    }

    /// <summary>Updates the current and minimum stock of a product.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductStockResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateProductStock(int id, [FromBody] UpdateProductStockResource resource,
        CancellationToken cancellationToken)
    {
        var updateProductStockCommand = UpdateProductStockCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await productStockCommandService.Handle(updateProductStockCommand, cancellationToken);
        return ActionResultFromUpdateProductStockResultAssembler.ToActionResultFromUpdateProductStockResult(
            result,
            this,
            localizer);
    }
}