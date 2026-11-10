using Microsoft.EntityFrameworkCore;
using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Inventory.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Inventory.Application.Internal.CommandServices;

/// <summary>Creates stock alerts.</summary>
public class StockAlertCommandService(
    IStockAlertRepository stockAlertRepository,
    IUnitOfWork unitOfWork,
    ILogger<StockAlertCommandService> logger)
    : IStockAlertCommandService
{
    /// <inheritdoc />
    public async Task<Result<StockAlert, CreateStockAlertError>> Handle(CreateStockAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stockAlert = new StockAlert(command);
            await stockAlertRepository.AddAsync(stockAlert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<StockAlert, CreateStockAlertError>.Success(stockAlert);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while creating alert for product {ProductId}", command.ProductId);
            return new Result<StockAlert, CreateStockAlertError>.Failure(CreateStockAlertError.InvalidStockAlertData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating alert for product {ProductId}", command.ProductId);
            return new Result<StockAlert, CreateStockAlertError>.Failure(CreateStockAlertError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating alert for product {ProductId}", command.ProductId);
            return new Result<StockAlert, CreateStockAlertError>.Failure(CreateStockAlertError.UnexpectedError);
        }
    }
}