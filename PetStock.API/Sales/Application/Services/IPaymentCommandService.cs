using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Sales.Application.Services;

/// <summary>Write operations for payments.</summary>
public interface IPaymentCommandService
{
    /// <summary>Registers a payment; returns the payment or the error.</summary>
    Task<Result<Payment, CreatePaymentError>> Handle(
        CreatePaymentCommand command,
        CancellationToken cancellationToken = default);
}