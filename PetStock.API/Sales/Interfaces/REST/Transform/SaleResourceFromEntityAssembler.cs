using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Interfaces.REST.Resources;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Converts a Sale into the resource returned by the API.</summary>
public static class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale entity) =>
        new(
            entity.Id,
            entity.UserId,
            entity.CustomerId,
            entity.PaymentMethod,
            entity.TotalAmount,
            entity.SaleDate,
            entity.Items
                .Select(item => new SaleItemResource(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice))
                .ToList());
}