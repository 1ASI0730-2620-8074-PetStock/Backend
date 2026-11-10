using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Catalog.Domain.Repositories;

/// <summary>Repository for suppliers.</summary>
public interface ISupplierRepository : IBaseRepository<Supplier>
{
}