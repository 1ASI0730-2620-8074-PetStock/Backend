using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Interfaces.REST.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Converts a GoodsReceipt into the resource returned by the API.
/// </summary>
public static class GoodsReceiptResourceFromEntityAssembler
{
    public static GoodsReceiptResource ToResourceFromEntity(
        GoodsReceipt entity) =>
        new(
            entity.Id,
            entity.PurchaseOrderId,
            entity.Notes,
            entity.ReceivedAt);
}