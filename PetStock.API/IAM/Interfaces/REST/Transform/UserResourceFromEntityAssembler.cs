using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Interfaces.REST.Resources;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity) =>
        new(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.Phone, entity.Role);
}