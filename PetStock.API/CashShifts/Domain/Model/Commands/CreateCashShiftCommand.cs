namespace PetStock.API.CashShifts.Domain.Model.Commands;

/// <summary>Data needed to open a cash shift.</summary>
public record CreateCashShiftCommand(int UserId, decimal OpeningAmount);
