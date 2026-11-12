using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Interfaces.REST.Resources;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Converts a PurchaseHistoryEntry into the resource returned by the API.</summary>
public static class PurchaseHistoryEntryResourceFromEntityAssembler
{
    public static PurchaseHistoryEntryResource ToResourceFromEntity(
        PurchaseHistoryEntry entity) =>
        new(
            entity.Id,
            entity.CustomerId,
            entity.SaleId,
            entity.TotalAmount,
            entity.PurchasedAt);
}