using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Interfaces.REST.Resources;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Converts the request body into a command.</summary>
public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(
        CreatePaymentResource resource) =>
        new(
            resource.SaleId,
            resource.Amount,
            resource.PaymentMethod.Trim());
}