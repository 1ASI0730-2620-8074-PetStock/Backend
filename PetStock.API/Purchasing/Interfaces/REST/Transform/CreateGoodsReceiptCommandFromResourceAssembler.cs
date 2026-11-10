using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Interfaces.REST.Resources;

namespace PetStock.API.Purchasing.Interfaces.REST.Transform;

/// <summary>
/// Converts the request body into a command.
/// </summary>
public static class CreateGoodsReceiptCommandFromResourceAssembler
{
    public static CreateGoodsReceiptCommand
        ToCommandFromResource(
            CreateGoodsReceiptResource resource) =>
        new(
            resource.PurchaseId,
            resource.Notes ?? string.Empty);
}