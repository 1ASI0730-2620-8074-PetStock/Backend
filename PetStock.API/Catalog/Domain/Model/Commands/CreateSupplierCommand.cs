namespace PetStock.API.Catalog.Domain.Model.Commands;

/// <summary>Data needed to register a supplier.</summary>
public record CreateSupplierCommand(string CompanyName, string ContactName, string Phone, string Email);
