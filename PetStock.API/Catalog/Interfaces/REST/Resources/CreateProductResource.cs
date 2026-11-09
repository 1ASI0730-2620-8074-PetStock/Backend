using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Body of POST /products. The product is always created as active.</summary>
public record CreateProductResource(
    [Required(ErrorMessage = "RequiredProductName")]
    [StringLength(150, ErrorMessage = "ProductNameTooLong")]
    string Name,
    [StringLength(255, ErrorMessage = "ProductDescriptionTooLong")]
    string? Description,
    [Range(0.01, 1000000.0, ErrorMessage = "InvalidProductPrice")]
    decimal Price,
    [Range(1, int.MaxValue, ErrorMessage = "InvalidProductCategoryId")]
    int CategoryId,
    [Range(1, int.MaxValue, ErrorMessage = "InvalidProductSupplierId")]
    int SupplierId);