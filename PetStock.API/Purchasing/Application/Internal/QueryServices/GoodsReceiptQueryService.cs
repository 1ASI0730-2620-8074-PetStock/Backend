using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Queries;
using PetStock.API.Purchasing.Domain.Repositories;

namespace PetStock.API.Purchasing.Application.Internal.QueryServices;

/// <summary>
/// Handles queries related to goods receipts.
/// </summary>
public class GoodsReceiptQueryService(
    IGoodsReceiptRepository goodsReceiptRepository)
    : IGoodsReceiptQueryService
{
    public async Task<IEnumerable<GoodsReceipt>>
        Handle(
            GetAllGoodsReceiptsQuery query,
            CancellationToken cancellationToken = default)
    {
        return await goodsReceiptRepository.ListAsync(
            cancellationToken);
    }

    public async Task<GoodsReceipt?>
        Handle(
            GetGoodsReceiptByIdQuery query,
            CancellationToken cancellationToken = default)
    {
        return await goodsReceiptRepository.FindByIdAsync(
            query.Id,
            cancellationToken);
    }
}