namespace PetStock.API.Purchasing.Interfaces.REST.Resources;

/// <summary>
/// Resource returned by the purchases API.
/// </summary>
public record PurchaseResource(int Id, int SupplierId, int ProductId,
    int Quantity,
    decimal UnitCost,
    decimal Total,
    string Status,
    DateTimeOffset Date);