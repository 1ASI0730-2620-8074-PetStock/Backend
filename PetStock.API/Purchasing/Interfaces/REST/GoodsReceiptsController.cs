using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Queries;
using PetStock.API.Purchasing.Interfaces.REST.Resources;
using PetStock.API.Purchasing.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST;

/// <summary>
/// Endpoints for the reception of purchased goods.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Goods Receipts")]
public class GoodsReceiptsController(
    IGoodsReceiptCommandService goodsReceiptCommandService,
    IGoodsReceiptQueryService goodsReceiptQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>
    /// Registers the reception of a purchase and marks it as received.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(GoodsReceiptResource),
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(string),
        StatusCodes.Status404NotFound)]
    [ProducesResponseType(
        typeof(string),
        StatusCodes.Status409Conflict)]
    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateGoodsReceipt(
        [FromBody] CreateGoodsReceiptResource resource,
        CancellationToken cancellationToken)
    {
        var createGoodsReceiptCommand =
            CreateGoodsReceiptCommandFromResourceAssembler
                .ToCommandFromResource(resource);

        var result =
            await goodsReceiptCommandService.Handle(
                createGoodsReceiptCommand,
                cancellationToken);

        return ActionResultFromCreateGoodsReceiptResultAssembler
            .ToActionResultFromCreateGoodsReceiptResult(
                result,
                this,
                localizer,
                nameof(GetGoodsReceiptById));
    }

    /// <summary>
    /// Lists all goods receipts.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IEnumerable<GoodsReceiptResource>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllGoodsReceipts(
        CancellationToken cancellationToken = default)
    {
        var getAllGoodsReceiptsQuery =
            new GetAllGoodsReceiptsQuery();

        var goodsReceipts =
            await goodsReceiptQueryService.Handle(
                getAllGoodsReceiptsQuery,
                cancellationToken);

        var resources =
            goodsReceipts.Select(
                GoodsReceiptResourceFromEntityAssembler
                    .ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>
    /// Gets one goods receipt by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(GoodsReceiptResource),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetGoodsReceiptById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var getGoodsReceiptByIdQuery =
            new GetGoodsReceiptByIdQuery(id);

        var goodsReceipt =
            await goodsReceiptQueryService.Handle(
                getGoodsReceiptByIdQuery,
                cancellationToken);

        if (goodsReceipt is null)
            return NotFound();

        var resource =
            GoodsReceiptResourceFromEntityAssembler
                .ToResourceFromEntity(goodsReceipt);

        return Ok(resource);
    }
}