using Microsoft.EntityFrameworkCore;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.CashShifts.Infrastructure.Persistence.EFC.Repositories;

public class CashReconciliationRepository(AppDbContext context) : BaseRepository<CashReconciliation>(context), ICashReconciliationRepository
{
    public async Task<bool> ExistsByCashShiftIdAsync(int cashShiftId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CashReconciliation>().AnyAsync(r => r.CashShiftId == cashShiftId, cancellationToken);
    }
}