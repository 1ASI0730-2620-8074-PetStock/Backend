using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Queries;

namespace PetStock.API.Customers.Application.Services;

/// <summary>Read operations for customers.</summary>
public interface ICustomerQueryService
{
    /// <summary>Lists all customers.</summary>
    Task<IEnumerable<Customer>> Handle(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>Gets one customer or null.</summary>
    Task<Customer?> Handle(
        GetCustomerByIdQuery query,
        CancellationToken cancellationToken = default);
}