using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Inventory.Domain.Repositories;

/// <summary>Repository for stock alerts.</summary>
public interface IStockAlertRepository : IBaseRepository<StockAlert>
{
}