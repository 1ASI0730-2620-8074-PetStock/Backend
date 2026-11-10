using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Purchasing.Application.Internal.CommandServices;

/// <summary>
/// Handles goods receipt commands.
/// </summary>
public class GoodsReceiptCommandService(
    IGoodsReceiptRepository goodsReceiptRepository,
    IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork,
    ILogger<GoodsReceiptCommandService> logger)
    : IGoodsReceiptCommandService
{
    public async Task<Result<GoodsReceipt, CreateGoodsReceiptError>>
        Handle(
            CreateGoodsReceiptCommand command,
            CancellationToken cancellationToken = default)
    {
        var purchaseOrder =
            await purchaseOrderRepository.FindByIdAsync(
                command.PurchaseOrderId,
                cancellationToken);

        if (purchaseOrder is null)
        {
            return new Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Failure(
                CreateGoodsReceiptError.PurchaseOrderNotFound);
        }

        if (purchaseOrder.IsReceived)
        {
            return new Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Failure(
                CreateGoodsReceiptError.PurchaseOrderAlreadyReceived);
        }

        try
        {
            var goodsReceipt =
                new GoodsReceipt(command);

            purchaseOrder.MarkAsReceived();

            await goodsReceiptRepository.AddAsync(
                goodsReceipt,
                cancellationToken);

            purchaseOrderRepository.Update(
                purchaseOrder);

            await unitOfWork.CompleteAsync(
                cancellationToken);

            return new Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Success(
                goodsReceipt);
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
                "Unexpected error creating goods receipt.");

            return new Result<
                GoodsReceipt,
                CreateGoodsReceiptError>.Failure(
                CreateGoodsReceiptError.UnexpectedError);
        }
    }
}