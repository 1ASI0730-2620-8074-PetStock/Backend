namespace PetStock.API.Inventory.Interfaces.REST.Resources;

/// <summary>Stock alert returned by the API.</summary>
public record StockAlertResource(int Id, int ProductId, string Level, bool Active, DateTimeOffset? CreatedAt);