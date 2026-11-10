using Microsoft.EntityFrameworkCore;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.CashShifts.Infrastructure.Persistence.EFC.Repositories;

public class CashShiftRepository(AppDbContext context) : BaseRepository<CashShift>(context), ICashShiftRepository
{
    public async Task<bool> ExistsOpenShiftByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CashShift>().AnyAsync(s => s.UserId == userId && s.Status == "Open", cancellationToken);
    }
}