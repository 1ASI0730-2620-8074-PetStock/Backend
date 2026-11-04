namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Category returned by the API.</summary>
public record CategoryResource(int Id, string Name, string Description);
