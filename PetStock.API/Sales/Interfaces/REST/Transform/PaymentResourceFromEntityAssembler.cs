using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Interfaces.REST.Resources;

namespace PetStock.API.Sales.Interfaces.REST.Transform;

/// <summary>Converts a Payment into the resource returned by the API.</summary>
public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment entity) =>
        new(
            entity.Id,
            entity.SaleId,
            entity.Amount,
            entity.PaymentMethod,
            entity.PaidAt);
}
