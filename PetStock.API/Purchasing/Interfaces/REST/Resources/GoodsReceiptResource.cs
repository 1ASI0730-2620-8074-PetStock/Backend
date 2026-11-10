namespace PetStock.API.Purchasing.Interfaces.REST.Resources;

/// <summary>
/// Goods receipt returned by the API.
/// </summary>
public record GoodsReceiptResource(
    int Id,
    int PurchaseId,
    string Notes,
    DateTimeOffset ReceivedAt);