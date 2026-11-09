namespace PetStock.API.Inventory.Domain.Model.Commands;

/// <summary>Data needed to update the current and minimum stock of a product.</summary>
public record UpdateProductStockCommand(int Id, int CurrentStock, int MinimumStock);