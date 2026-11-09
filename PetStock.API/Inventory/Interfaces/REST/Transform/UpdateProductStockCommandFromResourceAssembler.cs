using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Inventory.Interfaces.REST.Resources;

namespace PetStock.API.Inventory.Interfaces.REST.Transform;

/// <summary>Converts the route id and the request body into a command.</summary>
public static class UpdateProductStockCommandFromResourceAssembler
{
    public static UpdateProductStockCommand ToCommandFromResource(int id, UpdateProductStockResource resource) =>
        new(id, resource.CurrentStock, resource.MinimumStock);
}