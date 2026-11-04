namespace PetStock.API.Catalog.Domain.Model.Commands;

/// <summary>Data needed to create a product category.</summary>
public record CreateCategoryCommand(string Name, string Description);
