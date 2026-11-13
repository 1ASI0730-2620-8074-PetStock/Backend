
using Microsoft.EntityFrameworkCore;
using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Sales.Application.Internal.CommandServices;

/// <summary>Registers and deletes sales.</summary>
public class SaleCommandService(
    ISaleRepository saleRepository,
    IUnitOfWork unitOfWork,
    ILogger<SaleCommandService> logger)
    : ISaleCommandService
{
    /// <inheritdoc />
    public async Task<Result<Sale, CreateSaleError>> Handle(
        CreateSaleCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // The sale adds its items and calculates the total
            var sale = new Sale(command);

            await saleRepository.AddAsync(sale, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return new Result<Sale, CreateSaleError>.Success(sale);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(
                ex,
                "Invalid items while creating a sale for user {UserId}",
                command.UserId);

            return new Result<Sale, CreateSaleError>.Failure(
                CreateSaleError.InvalidSaleItems);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(
                ex,
                "Database update failed creating a sale for user {UserId}",
                command.UserId);

            return new Result<Sale, CreateSaleError>.Failure(
                CreateSaleError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(
        DeleteSaleCommand command,
        CancellationToken cancellationToken = default)
    {
        // The items are loaded so EF Core deletes them together with the sale
        var sale = await saleRepository.FindByIdWithItemsAsync(
            command.Id,
            cancellationToken);

        if (sale is null)
            return false;

        saleRepository.Remove(sale);
        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}
