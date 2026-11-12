namespace PetStock.API.Customers.Interfaces.REST.Resources;

/// <summary>Purchase history entry returned by the API.</summary>
public record PurchaseHistoryEntryResource(
    int Id,
    int CustomerId,
    int SaleId,
    decimal Total,
    DateTimeOffset PurchasedAt);