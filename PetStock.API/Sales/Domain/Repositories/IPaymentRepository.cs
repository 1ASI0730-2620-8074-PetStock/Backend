using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Sales.Domain.Repositories;

/// <summary>Repository for payments.</summary>
public interface IPaymentRepository : IBaseRepository<Payment>
{
}