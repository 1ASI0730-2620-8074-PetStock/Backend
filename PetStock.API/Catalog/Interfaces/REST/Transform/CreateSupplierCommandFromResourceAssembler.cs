using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateSupplierCommandFromResourceAssembler
{
    public static CreateSupplierCommand ToCommandFromResource(CreateSupplierResource resource) =>
        new(resource.CompanyName,
            resource.ContactName ?? string.Empty,
            resource.Phone ?? string.Empty,
            resource.Email ?? string.Empty);
}