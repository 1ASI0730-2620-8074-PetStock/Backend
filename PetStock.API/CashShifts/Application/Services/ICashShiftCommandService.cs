using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.CashShifts.Application.Services;

public interface ICashShiftCommandService
{
    Task<Result<CashShift, CreateCashShiftError>> Handle(CreateCashShiftCommand command, CancellationToken cancellationToken = default);
    Task<Result<CashShift, CloseCashShiftError>> Handle(CloseCashShiftCommand command, CancellationToken cancellationToken = default);
}