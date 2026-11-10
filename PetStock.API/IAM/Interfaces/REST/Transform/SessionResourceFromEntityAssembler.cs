using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Interfaces.REST.Resources;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class SessionResourceFromEntityAssembler
{
    public static SessionResource ToResourceFromEntity(Session entity) =>
        new(entity.Id, entity.UserId, entity.Token, entity.ExpiresAt);
}