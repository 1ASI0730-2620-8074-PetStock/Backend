using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Inventory.Interfaces.REST.Resources;

/// <summary>Body of POST /stock-alerts. The level is low or critical.</summary>
public record CreateStockAlertResource(
    [Range(1, int.MaxValue, ErrorMessage = "InvalidStockAlertProductId")]
    int ProductId,
    [Required(ErrorMessage = "RequiredStockAlertLevel")]
    [RegularExpression("^(low|critical)$", ErrorMessage = "InvalidStockAlertLevel")]
    string Level);