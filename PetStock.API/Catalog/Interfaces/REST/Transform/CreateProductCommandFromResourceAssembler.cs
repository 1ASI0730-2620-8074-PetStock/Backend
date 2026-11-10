using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource) =>
        new(resource.Name.Trim(),
            resource.Description?.Trim() ?? string.Empty,
            resource.Price,
            resource.CategoryId,
            resource.SupplierId);
}