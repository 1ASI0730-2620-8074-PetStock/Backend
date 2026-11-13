using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Purchasing.Interfaces.REST.Resources;

/// <summary>
/// Body of POST /goods-receipts. Notes are optional.
/// </summary>
public record CreateGoodsReceiptResource(
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "GoodsReceiptPurchaseIdInvalid")]
    int PurchaseId,

    [StringLength(
        255,
        ErrorMessage = "GoodsReceiptNotesTooLong")]
    string? Notes);