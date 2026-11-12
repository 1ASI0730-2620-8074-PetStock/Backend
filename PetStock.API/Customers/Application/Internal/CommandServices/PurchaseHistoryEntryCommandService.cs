
using Microsoft.EntityFrameworkCore;
using PetStock.API.Customers.Application.Errors;
using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Model.Commands;
using PetStock.API.Customers.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Customers.Application.Internal.CommandServices;

/// <summary>Saves purchases in the history of existing customers.</summary>
public class PurchaseHistoryEntryCommandService(
    IPurchaseHistoryEntryRepository purchaseHistoryEntryRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ILogger<PurchaseHistoryEntryCommandService> logger)
    : IPurchaseHistoryEntryCommandService
{
    /// <inheritdoc />
    public async Task<Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>> Handle(
        CreatePurchaseHistoryEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        // The entry must belong to an existing customer
        var customer = await customerRepository.FindByIdAsync(
            command.CustomerId,
            cancellationToken);

        if (customer is null)
        {
            return new Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>.Failure(
                CreatePurchaseHistoryEntryError.CustomerNotFound);
        }

        try
        {
            var entry = new PurchaseHistoryEntry(command);

            await purchaseHistoryEntryRepository.AddAsync(
                entry,
                cancellationToken);

            await unitOfWork.CompleteAsync(cancellationToken);

            return new Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>.Success(
                entry);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(
                ex,
                "Database update failed saving sale {SaleId} in the history",
                command.SaleId);

            return new Result<PurchaseHistoryEntry, CreatePurchaseHistoryEntryError>.Failure(
                CreatePurchaseHistoryEntryError.UnexpectedError);
        }
    }
}
