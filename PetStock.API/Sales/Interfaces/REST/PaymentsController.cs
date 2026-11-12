
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Resources;
using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Queries;
using PetStock.API.Sales.Interfaces.REST.Resources;
using PetStock.API.Sales.Interfaces.REST.Transform;

namespace PetStock.API.Sales.Interfaces.REST;

/// <summary>Endpoints for payments of sales.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Payments")]
public class PaymentsController(
    IPaymentCommandService paymentCommandService,
    IPaymentQueryService paymentQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Registers the payment of an existing sale.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreatePayment(
        [FromBody] CreatePaymentResource resource,
        CancellationToken cancellationToken)
    {
        var createPaymentCommand =
            CreatePaymentCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await paymentCommandService.Handle(
            createPaymentCommand,
            cancellationToken);

        return ActionResultFromCreatePaymentResultAssembler.ToActionResultFromCreatePaymentResult(
            result,
            this,
            localizer,
            nameof(GetPaymentById));
    }

    /// <summary>Lists all payments.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPayments(
        CancellationToken cancellationToken = default)
    {
        var getAllPaymentsQuery = new GetAllPaymentsQuery();

        var payments = await paymentQueryService.Handle(
            getAllPaymentsQuery,
            cancellationToken);

        var resources = payments.Select(
            PaymentResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>Gets one payment by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPaymentById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getPaymentByIdQuery = new GetPaymentByIdQuery(id);

        var payment = await paymentQueryService.Handle(
            getPaymentByIdQuery,
            cancellationToken);

        if (payment is null)
            return NotFound();

        var resource =
            PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);

        return Ok(resource);
    }
}
