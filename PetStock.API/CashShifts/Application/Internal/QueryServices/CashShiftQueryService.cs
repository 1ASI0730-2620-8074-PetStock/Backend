using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Queries;
using PetStock.API.CashShifts.Domain.Repositories;

namespace PetStock.API.CashShifts.Application.Internal.QueryServices;

public class CashShiftQueryService(ICashShiftRepository cashShiftRepository) : ICashShiftQueryService
{
    public async Task<IEnumerable<CashShift>> Handle(GetAllCashShiftsQuery query, CancellationToken cancellationToken = default) =>
        await cashShiftRepository.ListAsync(cancellationToken);

    public async Task<CashShift?> Handle(GetCashShiftByIdQuery query, CancellationToken cancellationToken = default) =>
        await cashShiftRepository.FindByIdAsync(query.Id, cancellationToken);
}