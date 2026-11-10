using System.ComponentModel.DataAnnotations;

namespace PetStock.API.CashShifts.Interfaces.REST.Resources;

public record CreateCashShiftResource(
    [Range(1, int.MaxValue, ErrorMessage = "CashShiftUserIdRequired")] int UserId,
    [Range(0.0, 99999999.0, ErrorMessage = "CashShiftOpeningAmountInvalid")] decimal OpeningAmount);