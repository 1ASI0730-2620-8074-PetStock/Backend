using PetStock.API.Purchasing.Application.Errors;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Purchasing.Application.Services;

/// <summary>
/// Handles commands related to goods receipts.
/// </summary>
public interface IGoodsReceiptCommandService
{
    Task<Result<GoodsReceipt, CreateGoodsReceiptError>>
        Handle(
            CreateGoodsReceiptCommand command,
            CancellationToken cancellationToken = default);
}