using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Sales.Infrastructure.Persistence.EFC.Repositories;

/// <summary>EF Core repository for payments.</summary>
public class PaymentRepository(AppDbContext context)
    : BaseRepository<Payment>(context), IPaymentRepository
{
}
