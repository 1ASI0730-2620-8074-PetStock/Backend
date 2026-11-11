using PetStock.API.Purchasing.Domain.Model.Aggregates;
using PetStock.API.Purchasing.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Purchasing.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// EF Core repository for goods receipts.
/// </summary>
public class GoodsReceiptRepository(AppDbContext context)
    : BaseRepository<GoodsReceipt>(context),
        IGoodsReceiptRepository
{
}