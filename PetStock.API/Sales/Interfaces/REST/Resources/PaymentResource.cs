namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>Payment returned by the API.</summary>
public record PaymentResource(
    int Id,
    int SaleId,
    decimal Amount,
    string PaymentMethod,
    DateTimeOffset PaidAt);