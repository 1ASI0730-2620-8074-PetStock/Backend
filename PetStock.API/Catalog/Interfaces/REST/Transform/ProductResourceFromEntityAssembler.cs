using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts a Product into the resource returned by the API.</summary>
public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product entity) =>
        new(entity.Id,
            entity.Name,
            entity.Description,
            entity.BasePrice,
            entity.CategoryId,
            entity.SupplierId,
            entity.IsActive);
}