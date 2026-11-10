using Microsoft.EntityFrameworkCore;
using PetStock.API.CashShifts.Application.Errors;
using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.CashShifts.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.CashShifts.Application.Internal.CommandServices;

public class CashShiftCommandService(
    ICashShiftRepository cashShiftRepository,
    IUnitOfWork unitOfWork,
    ILogger<CashShiftCommandService> logger)
    : ICashShiftCommandService
{
    public async Task<Result<CashShift, CreateCashShiftError>> Handle(CreateCashShiftCommand command, CancellationToken cancellationToken = default)
    {
        if (await cashShiftRepository.ExistsOpenShiftByUserIdAsync(command.UserId, cancellationToken))
        {
            logger.LogWarning("User {UserId} already has an open cash shift", command.UserId);
            return new Result<CashShift, CreateCashShiftError>.Failure(CreateCashShiftError.UserAlreadyHasOpenShift);
        }

        try
        {
            var cashShift = new CashShift(command);
            await cashShiftRepository.AddAsync(cashShift, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<CashShift, CreateCashShiftError>.Success(cashShift);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while opening a cash shift for user {UserId}", command.UserId);
            return new Result<CashShift, CreateCashShiftError>.Failure(CreateCashShiftError.InvalidCashShiftData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed opening a cash shift for user {UserId}", command.UserId);
            return new Result<CashShift, CreateCashShiftError>.Failure(CreateCashShiftError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error opening a cash shift for user {UserId}", command.UserId);
            return new Result<CashShift, CreateCashShiftError>.Failure(CreateCashShiftError.UnexpectedError);
        }
    }

    public async Task<Result<CashShift, CloseCashShiftError>> Handle(CloseCashShiftCommand command, CancellationToken cancellationToken = default)
    {
        var cashShift = await cashShiftRepository.FindByIdAsync(command.CashShiftId, cancellationToken);
        if (cashShift is null)
            return new Result<CashShift, CloseCashShiftError>.Failure(CloseCashShiftError.CashShiftNotFound);

        try
        {
            cashShift.Close(command.ClosingAmount);
            cashShiftRepository.Update(cashShift);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<CashShift, CloseCashShiftError>.Success(cashShift);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Cash shift {CashShiftId} is already closed", command.CashShiftId);
            return new Result<CashShift, CloseCashShiftError>.Failure(CloseCashShiftError.CashShiftAlreadyClosed);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid closing amount for cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashShift, CloseCashShiftError>.Failure(CloseCashShiftError.InvalidClosingAmount);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed closing cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashShift, CloseCashShiftError>.Failure(CloseCashShiftError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error closing cash shift {CashShiftId}", command.CashShiftId);
            return new Result<CashShift, CloseCashShiftError>.Failure(CloseCashShiftError.UnexpectedError);
        }
    }
}