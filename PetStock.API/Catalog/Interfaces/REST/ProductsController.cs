using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Interfaces.REST.Resources;
using PetStock.API.Catalog.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Catalog.Interfaces.REST;

/// <summary>Endpoints for products.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Products")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates a product. Its category and supplier must exist.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProductResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductResource resource,
        CancellationToken cancellationToken)
    {
        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await productCommandService.Handle(createProductCommand, cancellationToken);
        return ActionResultFromCreateProductResultAssembler.ToActionResultFromCreateProductResult(
            result,
            this,
            localizer,
            nameof(GetProductById));
    }

    /// <summary>Lists all products.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var getAllProductsQuery = new GetAllProductsQuery();
        var products = await productQueryService.Handle(getAllProductsQuery, cancellationToken);
        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one product by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProductById(int id, CancellationToken cancellationToken = default)
    {
        var getProductByIdQuery = new GetProductByIdQuery(id);
        var product = await productQueryService.Handle(getProductByIdQuery, cancellationToken);
        if (product is null) return NotFound();
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(resource);
    }
}