using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Queries;
using PetStock.API.Customers.Domain.Repositories;

namespace PetStock.API.Customers.Application.Internal.QueryServices;

/// <summary>Reads customers from the repository.</summary>
public class CustomerQueryService(ICustomerRepository customerRepository)
    : ICustomerQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Customer>> Handle(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default) =>
        await customerRepository.ListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Customer?> Handle(
        GetCustomerByIdQuery query,
        CancellationToken cancellationToken = default) =>
        await customerRepository.FindByIdAsync(
            query.Id,
            cancellationToken);
}