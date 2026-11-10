using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.CashShifts.Domain.Repositories;

public interface ICashShiftRepository : IBaseRepository<CashShift>
{
    Task<bool> ExistsOpenShiftByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}