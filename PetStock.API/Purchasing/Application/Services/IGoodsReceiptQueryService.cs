using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Model.Queries;

namespace PetStock.API.Purchasing.Application.Services;

/// <summary>
/// Handles queries related to goods receipts.
/// </summary>
public interface IGoodsReceiptQueryService
{
    Task<IEnumerable<GoodsReceipt>>
        Handle(
            GetAllGoodsReceiptsQuery query,
            CancellationToken cancellationToken = default);

    Task<GoodsReceipt?>
        Handle(
            GetGoodsReceiptByIdQuery query,
            CancellationToken cancellationToken = default);
}