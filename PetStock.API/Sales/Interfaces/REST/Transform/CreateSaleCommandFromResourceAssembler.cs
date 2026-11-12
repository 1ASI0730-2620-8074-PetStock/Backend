using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Interfaces.REST.Resources;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreateSaleCommandFromResourceAssembler
{
    public static CreateSaleCommand ToCommandFromResource(
        CreateSaleResource resource) =>
        new(
            resource.UserId,
            resource.CustomerId,
            resource.PaymentMethod.Trim(),
            resource.Items
                .Select(item => new CreateSaleItemCommand(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice))
                .ToList());
}