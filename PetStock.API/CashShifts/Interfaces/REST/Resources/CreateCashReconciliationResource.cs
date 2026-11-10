using System.ComponentModel.DataAnnotations;

namespace PetStock.API.CashShifts.Interfaces.REST.Resources;

public record CreateCashReconciliationResource(
    [Range(1, int.MaxValue, ErrorMessage = "CashReconciliationCashShiftIdRequired")] int CashShiftId,
    [Range(0.0, 99999999.0, ErrorMessage = "CashReconciliationAmountInvalid")] decimal ExpectedAmount,
    [Range(0.0, 99999999.0, ErrorMessage = "CashReconciliationAmountInvalid")] decimal ActualAmount);