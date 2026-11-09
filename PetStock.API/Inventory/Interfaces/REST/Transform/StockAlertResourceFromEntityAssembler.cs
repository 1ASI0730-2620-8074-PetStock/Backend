using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Interfaces.REST.Resources;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Converts a StockAlert into the resource returned by the API.</summary>
public static class StockAlertResourceFromEntityAssembler
{
    public static StockAlertResource ToResourceFromEntity(StockAlert entity) =>
        new(entity.Id, entity.ProductId, entity.AlertLevel, entity.IsActive, entity.CreatedAt);
}