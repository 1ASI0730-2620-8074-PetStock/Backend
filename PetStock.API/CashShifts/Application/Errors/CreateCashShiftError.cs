namespace PetStock.API.CashShifts.Application.Errors;

public enum CreateCashShiftError
{
    InvalidCashShiftData,
    UserAlreadyHasOpenShift,
    UnexpectedError
}