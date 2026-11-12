using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Queries;
using PetStock.API.Sales.Domain.Repositories;

namespace PetStock.API.Sales.Application.Internal.QueryServices;

/// <summary>Reads payments from the repository.</summary>
public class PaymentQueryService(IPaymentRepository paymentRepository)
    : IPaymentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Payment>> Handle(
        GetAllPaymentsQuery query,
        CancellationToken cancellationToken = default) =>
        await paymentRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Payment?> Handle(
        GetPaymentByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await paymentRepository.FindByIdAsync(
            query.Id,
            cancellationToken);
}