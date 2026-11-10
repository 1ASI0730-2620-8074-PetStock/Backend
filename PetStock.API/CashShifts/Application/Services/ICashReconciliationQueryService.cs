using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Queries;

namespace PetStock.API.CashShifts.Application.Services;

public interface ICashReconciliationQueryService
{
    Task<IEnumerable<CashReconciliation>> Handle(GetAllCashReconciliationsQuery query, CancellationToken cancellationToken = default);
    Task<CashReconciliation?> Handle(GetCashReconciliationByIdQuery query, CancellationToken cancellationToken = default);
}