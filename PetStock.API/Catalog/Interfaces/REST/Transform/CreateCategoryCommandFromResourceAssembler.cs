using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateCategoryCommandFromResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource) =>
        new(resource.Name, resource.Description ?? string.Empty);
}
