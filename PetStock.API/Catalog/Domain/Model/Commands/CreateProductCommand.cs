namespace PetStock.API.Catalog.Domain.Model.Commands;

/// <summary>Data needed to register a product in the catalog.</summary>
public record CreateProductCommand(
    string Name,
    string Description,
    decimal BasePrice,
    int CategoryId,
    int SupplierId);
