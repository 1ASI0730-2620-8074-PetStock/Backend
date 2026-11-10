using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Inventory.Interfaces.REST.Resources;

/// <summary>Body of PUT /inventories/{id}. Both quantities are replaced.</summary>
public record UpdateProductStockResource(
    [Range(0, int.MaxValue, ErrorMessage = "InvalidProductStockQuantity")]
    int CurrentStock,
    [Range(0, int.MaxValue, ErrorMessage = "InvalidProductStockQuantity")]
    int MinimumStock);