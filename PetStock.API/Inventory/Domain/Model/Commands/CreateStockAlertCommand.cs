namespace PetStock.API.Inventory.Domain.Model.Commands;

/// <summary>Data needed to raise a stock alert.</summary>
public record CreateStockAlertCommand(int ProductId, string AlertLevel);
