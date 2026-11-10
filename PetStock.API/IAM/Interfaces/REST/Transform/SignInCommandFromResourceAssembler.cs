using PetStock.API.IAM.Domain.Model.Commands;
using PetStock.API.IAM.Interfaces.REST.Resources;

namespace PetStock.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource) =>
        new(resource.Email, resource.Password);
}