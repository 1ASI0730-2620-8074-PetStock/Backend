using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.IAM.Interfaces.REST.Resources;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) =>
        new(resource.FirstName, resource.LastName, resource.Email, resource.Password, resource.Phone ?? string.Empty, string.IsNullOrWhiteSpace(resource.Role) ? "Owner" : resource.Role);
}