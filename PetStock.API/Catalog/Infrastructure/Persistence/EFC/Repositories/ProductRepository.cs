using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Catalog.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for products.</summary>
public class ProductRepository(AppDbContext context)
    : BaseRepository<Product>(context), IProductRepository
{
}