using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Queries;

namespace PetStock.API.CashShifts.Application.Services;

public interface ICashShiftQueryService
{
    Task<IEnumerable<CashShift>> Handle(GetAllCashShiftsQuery query, CancellationToken cancellationToken = default);
    Task<CashShift?> Handle(GetCashShiftByIdQuery query, CancellationToken cancellationToken = default);
}