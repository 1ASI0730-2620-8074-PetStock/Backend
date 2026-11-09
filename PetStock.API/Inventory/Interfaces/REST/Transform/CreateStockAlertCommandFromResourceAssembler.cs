using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Inventory.Interfaces.REST.Resources;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateStockAlertCommandFromResourceAssembler
{
    public static CreateStockAlertCommand ToCommandFromResource(CreateStockAlertResource resource) =>
        new(resource.ProductId, resource.Level);
}