using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts a Category into the resource returned by the API.</summary>
public static class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category entity) =>
        new(entity.Id, entity.Name, entity.Description);
}
