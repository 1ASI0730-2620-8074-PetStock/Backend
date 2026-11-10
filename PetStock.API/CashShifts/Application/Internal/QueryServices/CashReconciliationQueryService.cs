using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Queries;
using PetStock.API.CashShifts.Domain.Repositories;

namespace PetStock.API.CashShifts.Application.Internal.QueryServices;

public class CashReconciliationQueryService(ICashReconciliationRepository cashReconciliationRepository) : ICashReconciliationQueryService
{
    public async Task<IEnumerable<CashReconciliation>> Handle(GetAllCashReconciliationsQuery query, CancellationToken cancellationToken = default) =>
        await cashReconciliationRepository.ListAsync(cancellationToken);

    public async Task<CashReconciliation?> Handle(GetCashReconciliationByIdQuery query, CancellationToken cancellationToken = default) =>
        await cashReconciliationRepository.FindByIdAsync(query.Id, cancellationToken);
}