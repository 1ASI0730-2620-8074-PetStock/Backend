using System.ComponentModel.DataAnnotations;

namespace PetStock.API.CashShifts.Interfaces.REST.Resources;

public record CloseCashShiftResource(
    [Range(0.0, 99999999.0, ErrorMessage = "CashShiftClosingAmountInvalid")] decimal ClosingAmount);