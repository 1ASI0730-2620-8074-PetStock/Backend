using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Customers.Domain.Repositories;

/// <summary>Repository for purchase history entries.</summary>
public interface IPurchaseHistoryEntryRepository
    : IBaseRepository<PurchaseHistoryEntry>
{
}
