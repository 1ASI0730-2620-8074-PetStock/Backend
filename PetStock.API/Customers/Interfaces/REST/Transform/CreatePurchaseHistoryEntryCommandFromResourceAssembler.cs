using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Customers.Interfaces.REST.Resources;

namespace PetStock.API.Customers.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreatePurchaseHistoryEntryCommandFromResourceAssembler
{
    public static CreatePurchaseHistoryEntryCommand ToCommandFromResource(
        CreatePurchaseHistoryEntryResource resource) =>
        new(
            resource.CustomerId,
            resource.SaleId,
            resource.Total);
}