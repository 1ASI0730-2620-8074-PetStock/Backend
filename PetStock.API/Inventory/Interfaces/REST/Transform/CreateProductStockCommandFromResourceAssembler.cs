using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Inventory.Interfaces.REST.Resources;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateProductStockCommandFromResourceAssembler
{
    public static CreateProductStockCommand ToCommandFromResource(CreateProductStockResource resource) =>
        new(resource.ProductId, resource.CurrentStock, resource.MinimumStock);
}