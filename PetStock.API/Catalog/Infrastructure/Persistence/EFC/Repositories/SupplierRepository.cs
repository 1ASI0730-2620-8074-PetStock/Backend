using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Catalog.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for suppliers.</summary>
public class SupplierRepository(AppDbContext context)
    : BaseRepository<Supplier>(context), ISupplierRepository
{
}