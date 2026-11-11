using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Purchasing.Domain.Repositories;

/// <summary>
/// Repository for goods receipts.
/// </summary>
public interface IGoodsReceiptRepository
    : IBaseRepository<GoodsReceipt>
{
}