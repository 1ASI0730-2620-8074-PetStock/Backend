using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Inventory.Interfaces.REST.Resources;

/// <summary>Body of POST /inventories.</summary>
public record CreateProductStockResource(
    [Range(1, int.MaxValue, ErrorMessage = "InvalidProductStockProductId")]
    int ProductId,
    [Range(0, int.MaxValue, ErrorMessage = "InvalidProductStockQuantity")]
    int CurrentStock,
    [Range(0, int.MaxValue, ErrorMessage = "InvalidProductStockQuantity")]
    int MinimumStock);