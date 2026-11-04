namespace PetStock.API.Inventory.Domain.Model.Commands;

/// <summary>Data needed to register the stock of a product.</summary>
public record CreateProductStockCommand(int ProductId, int CurrentStock, int MinimumStock);
