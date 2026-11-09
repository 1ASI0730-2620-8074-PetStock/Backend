using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Interfaces.REST.Resources;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Converts a ProductStock into the resource returned by the API.</summary>
public static class ProductStockResourceFromEntityAssembler
{
    public static ProductStockResource ToResourceFromEntity(ProductStock entity) =>
        new(entity.Id, entity.ProductId, entity.CurrentStock, entity.MinimumStock, entity.UpdatedAt);
}