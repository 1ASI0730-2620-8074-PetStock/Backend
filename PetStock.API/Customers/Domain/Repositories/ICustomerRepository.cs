using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Customers.Domain.Repositories;

/// <summary>Repository for customers.</summary>
public interface ICustomerRepository : IBaseRepository<Customer>
{
    /// <summary>True if a customer with that email already exists.</summary>
    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}