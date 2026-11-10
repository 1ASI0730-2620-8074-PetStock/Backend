using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.IAM.Interfaces.REST.Resources;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(int userId, UpdateUserResource resource) =>
        new(userId, resource.FirstName, resource.LastName, resource.Email, resource.Phone ?? string.Empty);
}