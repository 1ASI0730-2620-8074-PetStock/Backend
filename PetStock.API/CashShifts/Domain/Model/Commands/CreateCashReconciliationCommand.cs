namespace PetStock.API.CashShifts.Domain.Model.Commands;

/// <summary>Data needed to compare expected and counted cash of a shift.</summary>
public record CreateCashReconciliationCommand(int CashShiftId, decimal ExpectedAmount, decimal ActualAmount);
