using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Customers.Application.Services;

/// <summary>Write operations for customers.</summary>
public interface ICustomerCommandService
{
    /// <summary>Creates a customer; returns the customer or the error.</summary>
    Task<Result<Customer, CreateCustomerError>> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken = default);
}