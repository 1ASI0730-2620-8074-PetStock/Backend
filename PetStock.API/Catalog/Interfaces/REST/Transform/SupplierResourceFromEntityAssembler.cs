using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Interfaces.REST.Resources;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Converts a Supplier into the resource returned by the API.</summary>
public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier entity) =>
        new(entity.Id, entity.CompanyName, entity.ContactName, entity.Phone, entity.Email);
}