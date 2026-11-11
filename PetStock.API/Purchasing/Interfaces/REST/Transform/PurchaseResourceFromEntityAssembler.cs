using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Interfaces.REST.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Converts a PurchaseOrder entity into a PurchaseResource.
/// </summary>
public static class PurchaseResourceFromEntityAssembler
{
    public static PurchaseResource ToResourceFromEntity(
        PurchaseOrder entity)
    {
        var item = entity.Items.FirstOrDefault();

        var quantity = item?.Quantity ?? 0;
        var unitCost = item?.UnitCost ?? 0m;
        var total = quantity * unitCost;
        var productId = item?.ProductId ?? 0;

        return new PurchaseResource(entity.Id, entity.SupplierId, productId, quantity,
            unitCost, total, entity.Status, entity.OrderDate);
    }
}