
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Queries;
using PetStock.API.Customers.Interfaces.REST.Resources;
using PetStock.API.Customers.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Customers.Interfaces.REST;

/// <summary>Endpoints for recurring customers.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Customers")]
public class CustomersController(
    ICustomerCommandService customerCommandService,
    ICustomerQueryService customerQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates a customer. The email cannot be repeated.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCustomer(
        [FromBody] CreateCustomerResource resource,
        CancellationToken cancellationToken)
    {
        var createCustomerCommand =
            CreateCustomerCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await customerCommandService.Handle(
            createCustomerCommand,
            cancellationToken);

        return ActionResultFromCreateCustomerResultAssembler.ToActionResultFromCreateCustomerResult(
            result,
            this,
            localizer,
            nameof(GetCustomerById));
    }

    /// <summary>Lists all customers.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCustomers(
        CancellationToken cancellationToken = default)
    {
        var getAllCustomersQuery = new GetAllCustomersQuery();

        var customers = await customerQueryService.Handle(
            getAllCustomersQuery,
            cancellationToken);

        var resources = customers.Select(
            CustomerResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>Gets one customer by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CustomerResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCustomerById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getCustomerByIdQuery = new GetCustomerByIdQuery(id);

        var customer = await customerQueryService.Handle(
            getCustomerByIdQuery,
            cancellationToken);

        if (customer is null)
            return NotFound();

        var resource =
            CustomerResourceFromEntityAssembler.ToResourceFromEntity(customer);

        return Ok(resource);
    }
}