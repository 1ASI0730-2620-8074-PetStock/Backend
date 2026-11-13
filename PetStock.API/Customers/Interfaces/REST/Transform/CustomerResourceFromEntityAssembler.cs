using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Interfaces.REST.Resources;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Converts a Customer into the resource returned by the API.</summary>
public static class CustomerResourceFromEntityAssembler
{
    public static CustomerResource ToResourceFromEntity(Customer entity) =>
        new(
            entity.Id,
            entity.FullName,
            entity.Email,
            entity.Phone,
            entity.Address);
}