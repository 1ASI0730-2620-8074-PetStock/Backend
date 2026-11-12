using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Customers.Interfaces.REST.Resources;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command. Empty optional fields become "".</summary>
public static class CreateCustomerCommandFromResourceAssembler
{
    public static CreateCustomerCommand ToCommandFromResource(
        CreateCustomerResource resource) =>
        new(
            resource.Name.Trim(),
            resource.Email?.Trim() ?? string.Empty,
            resource.Phone?.Trim() ?? string.Empty,
            resource.Address?.Trim() ?? string.Empty);
}