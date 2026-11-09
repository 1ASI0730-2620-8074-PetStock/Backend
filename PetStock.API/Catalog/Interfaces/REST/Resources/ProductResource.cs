namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Product returned by the API.</summary>
public record ProductResource(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    int SupplierId,
    bool Active);