
using Microsoft.EntityFrameworkCore;
using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Customers.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Customers.Application.Internal.CommandServices;

/// <summary>Creates customers and rejects repeated emails.</summary>
public class CustomerCommandService(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ILogger<CustomerCommandService> logger)
    : ICustomerCommandService
{
    /// <inheritdoc />
    public async Task<Result<Customer, CreateCustomerError>> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        // The email is optional, but when it is sent it cannot be repeated
        if (!string.IsNullOrWhiteSpace(command.Email) &&
            await customerRepository.ExistsByEmailAsync(
                command.Email,
                cancellationToken))
        {
            logger.LogWarning(
                "Duplicate customer rejected for email {CustomerEmail}",
                command.Email);

            return new Result<Customer, CreateCustomerError>.Failure(
                CreateCustomerError.DuplicateCustomerEmail);
        }

        try
        {
            var customer = new Customer(command);

            await customerRepository.AddAsync(
                customer,
                cancellationToken);

            await unitOfWork.CompleteAsync(cancellationToken);

            return new Result<Customer, CreateCustomerError>.Success(
                customer);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(
                ex,
                "Database update failed creating customer {CustomerName}",
                command.FullName);

            return new Result<Customer, CreateCustomerError>.Failure(
                CreateCustomerError.UnexpectedError);
        }
    }
}
