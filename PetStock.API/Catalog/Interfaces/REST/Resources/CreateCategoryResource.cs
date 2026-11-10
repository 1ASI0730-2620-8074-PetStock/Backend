using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Body of POST /categories. Only the name is required.</summary>
public record CreateCategoryResource(
    [Required(ErrorMessage = "RequiredCategoryName")]
    [StringLength(100, ErrorMessage = "CategoryNameTooLong")]
    string Name,
    string? Description);
