using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.CashShifts.Domain.Repositories;

public interface ICashReconciliationRepository : IBaseRepository<CashReconciliation>
{
    Task<bool> ExistsByCashShiftIdAsync(int cashShiftId, CancellationToken cancellationToken = default);
}