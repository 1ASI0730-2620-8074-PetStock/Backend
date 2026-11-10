using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.CashShifts.Application.Services;

public interface ICashReconciliationCommandService
{
    Task<Result<CashReconciliation, CreateCashReconciliationError>> Handle(CreateCashReconciliationCommand command, CancellationToken cancellationToken = default);
}