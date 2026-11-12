using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Queries;

namespace PetStock.API.Sales.Application.Services;

/// <summary>Read operations for payments.</summary>
public interface IPaymentQueryService
{
    /// <summary>Lists all payments.</summary>
    Task<IEnumerable<Payment>> Handle(
        GetAllPaymentsQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one payment or null.</summary>
    Task<Payment?> Handle(
        GetPaymentByIdQuery query,
        CancellationToken cancellationToken = default);
}