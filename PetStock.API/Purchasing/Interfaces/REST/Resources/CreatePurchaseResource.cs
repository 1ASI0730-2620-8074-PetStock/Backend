using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Purchasing.Interfaces.REST.Resources;

/// <summary>
/// Resource used to create a purchase order.
/// </summary>
public record CreatePurchaseResource(
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "PurchaseSupplierIdInvalid")]
    int SupplierId,

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "PurchaseProductIdInvalid")]
    int ProductId,

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "PurchaseQuantityInvalid")]
    int Quantity,

    [Range(
        0.01,
        double.MaxValue,
        ErrorMessage = "PurchaseUnitCostInvalid")]
    decimal UnitCost);