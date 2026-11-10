using Microsoft.EntityFrameworkCore;
using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.CashShifts.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.CashShifts.Application.Internal.CommandServices;

public class CashReconciliationCommandService(
    ICashReconciliationRepository cashReconciliationRepository,
    ICashShiftRepository cashShiftRepository,
    IUnitOfWork unitOfWork,
    ILogger<CashReconciliationCommandService> logger)
    : ICashReconciliationCommandService
{
    public async Task<Result<CashReconciliation, CreateCashReconciliationError>> Handle(
        CreateCashReconciliationCommand command,
        CancellationToken cancellationToken = default)
    {
        var cashShift = await cashShiftRepository.FindByIdAsync(command.CashShiftId, cancellationToken);
        if (cashShift is null)
            return new Result<CashReconciliation, CreateCashReconciliationError>.Failure(CreateCashReconciliationError.CashShiftNotFound);

        if (await cashReconciliationRepository.ExistsByCashShiftIdAsync(command.CashShiftId, cancellationToken))
        {
            logger.LogWarning("Duplicate reconciliation rejected for cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashReconciliation, CreateCashReconciliationError>.Failure(CreateCashReconciliationError.DuplicateCashReconciliation);
        }

        try
        {
            var cashReconciliation = new CashReconciliation(command);
            await cashReconciliationRepository.AddAsync(cashReconciliation, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<CashReconciliation, CreateCashReconciliationError>.Success(cashReconciliation);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating reconciliation for cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashReconciliation, CreateCashReconciliationError>.Failure(CreateCashReconciliationError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating reconciliation for cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashReconciliation, CreateCashReconciliationError>.Failure(CreateCashReconciliationError.UnexpectedError);
        }
    }
}