using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Interfaces.REST.Resources;
using PetStock.API.Catalog.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Catalog.Interfaces.REST;

/// <summary>Endpoints for suppliers.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Suppliers")]
public class SuppliersController(
    ISupplierCommandService supplierCommandService,
    ISupplierQueryService supplierQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates a supplier.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(SupplierResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateSupplier([FromBody] CreateSupplierResource resource,
        CancellationToken cancellationToken)
    {
        var createSupplierCommand = CreateSupplierCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await supplierCommandService.Handle(createSupplierCommand, cancellationToken);
        return ActionResultFromCreateSupplierResultAssembler.ToActionResultFromCreateSupplierResult(
            result, this, localizer, nameof(GetSupplierById));
    }

    /// <summary>Lists all suppliers.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllSuppliers(CancellationToken cancellationToken = default)
    {
        var getAllSuppliersQuery = new GetAllSuppliersQuery();
        var suppliers = await supplierQueryService.Handle(getAllSuppliersQuery, cancellationToken);
        var resources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one supplier by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SupplierResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSupplierById(int id, CancellationToken cancellationToken = default)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(id);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery, cancellationToken);
        if (supplier is null) return NotFound();
        var resource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return Ok(resource);
    }
}