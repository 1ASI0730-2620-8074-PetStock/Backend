namespace PetStock.API.Inventory.Interfaces.REST.Resources;

/// <summary>Stock of a product returned by the API.</summary>
public record ProductStockResource(
    int Id,
    int ProductId,
    int CurrentStock,
    int MinimumStock,
    DateTimeOffset? LastUpdated);