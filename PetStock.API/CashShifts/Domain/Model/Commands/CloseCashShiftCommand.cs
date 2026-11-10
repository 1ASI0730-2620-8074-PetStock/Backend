namespace PetStock.API.CashShifts.Domain.Model.Commands;

public record CloseCashShiftCommand(int CashShiftId, decimal ClosingAmount);