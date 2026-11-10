using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Purchasing.Application.Internal.CommandServices;

/// <summary>
/// Handles commands related to purchase orders.
/// </summary>
public class PurchaseOrderCommandService(
    IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork,
    ILogger<PurchaseOrderCommandService> logger)
    : IPurchaseOrderCommandService
{
    /// <summary>
    /// Creates a new purchase order.
    /// </summary>
    public async Task<Result<PurchaseOrder, CreatePurchaseOrderError>>
        Handle(
            CreatePurchaseOrderCommand command,
            CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.SupplierId <= 0 ||
                command.ProductId <= 0 ||
                command.Quantity <= 0 ||
                command.UnitCost <= 0)
            {
                return new Result<
                    PurchaseOrder,
                    CreatePurchaseOrderError>.Failure(
                    CreatePurchaseOrderError.InvalidPurchaseData);
            }

            var purchaseOrder =
                new PurchaseOrder(command);

            purchaseOrder.AddItem(
                command.ProductId,
                command.Quantity,
                command.UnitCost);

            await purchaseOrderRepository.AddAsync(
                purchaseOrder,
                cancellationToken);

            await unitOfWork.CompleteAsync(
                cancellationToken);

            return new Result<
                PurchaseOrder,
                CreatePurchaseOrderError>.Success(
                purchaseOrder);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Unexpected error creating purchase order.");

            return new Result<
                PurchaseOrder,
                CreatePurchaseOrderError>.Failure(
                CreatePurchaseOrderError.UnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a purchase order by id.
    /// </summary>
    public async Task<bool> Handle(
        DeletePurchaseOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var purchaseOrder =
            await purchaseOrderRepository.FindByIdAsync(
                command.Id,
                cancellationToken);

        if (purchaseOrder is null)
            return false;

        purchaseOrderRepository.Remove(
            purchaseOrder);

        await unitOfWork.CompleteAsync(
            cancellationToken);

        return true;
    }
}