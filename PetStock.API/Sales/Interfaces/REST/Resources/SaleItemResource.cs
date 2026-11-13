namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>One line of a sale returned by the API.</summary>
public record SaleItemResource(
    int ProductId,
    int Quantity,
    decimal UnitPrice);