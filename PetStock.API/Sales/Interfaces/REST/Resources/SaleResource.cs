namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>Sale returned by the API.</summary>
public record SaleResource(
    int Id,
    int UserId,
    int? CustomerId,
    string PaymentMethod,
    decimal Total,
    DateTimeOffset Date,
    IEnumerable<SaleItemResource> Items);