using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Customers.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for purchase history entries.</summary>
public class PurchaseHistoryEntryRepository(AppDbContext context)
    : BaseRepository<PurchaseHistoryEntry>(context),
        IPurchaseHistoryEntryRepository
{
}
