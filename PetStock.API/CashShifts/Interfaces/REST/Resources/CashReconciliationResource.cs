namespace PetStock.API.CashShifts.Interfaces.REST.Resources;

public record CashReconciliationResource(int Id, int CashShiftId, decimal ExpectedAmount, decimal ActualAmount, decimal Difference);