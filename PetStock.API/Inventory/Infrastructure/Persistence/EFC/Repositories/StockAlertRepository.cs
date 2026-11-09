using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for stock alerts.</summary>
public class StockAlertRepository(AppDbContext context)
    : BaseRepository<StockAlert>(context), IStockAlertRepository
{
}