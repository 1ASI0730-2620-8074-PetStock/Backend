using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>One product line in the body of POST /sales.</summary>
public record CreateSaleItemResource(
    [Range(1, int.MaxValue, ErrorMessage = "SaleItemProductIdInvalid")]
    int ProductId,

    [Range(1, int.MaxValue, ErrorMessage = "SaleItemQuantityInvalid")]
    int Quantity,

    [Range(0.0, 999999.99, ErrorMessage = "SaleItemUnitPriceInvalid")]
    decimal UnitPrice);