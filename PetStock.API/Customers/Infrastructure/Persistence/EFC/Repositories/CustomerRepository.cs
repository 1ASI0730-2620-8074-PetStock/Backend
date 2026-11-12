using Microsoft.EntityFrameworkCore;
using PetStock.API.Customers.Domain.Model.Aggregates;
using PetStock.API.Customers.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Customers.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for customers.</summary>
public class CustomerRepository(AppDbContext context)
    : BaseRepository<Customer>(context), ICustomerRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Customer>()
            .AnyAsync(
                c => c.Email == email,
                cancellationToken);
    }
}