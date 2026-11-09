namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Supplier returned by the API.</summary>
public record SupplierResource(int Id, string CompanyName, string ContactName, string Phone, string Email);