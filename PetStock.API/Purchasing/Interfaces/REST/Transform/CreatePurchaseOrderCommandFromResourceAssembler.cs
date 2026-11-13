using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Interfaces.REST.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Converts a CreatePurchaseResource into a CreatePurchaseOrderCommand.
/// </summary>
public static class CreatePurchaseOrderCommandFromResourceAssembler
{
    public static CreatePurchaseOrderCommand ToCommandFromResource(
        CreatePurchaseResource resource)
    {
        return new CreatePurchaseOrderCommand(
            resource.SupplierId,
            resource.ProductId,
            resource.Quantity,
            resource.UnitCost);
    }
}