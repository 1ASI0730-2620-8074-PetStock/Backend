namespace PetStock.API.CashShifts.Application.Errors;

public enum CloseCashShiftError
{
    CashShiftNotFound,
    CashShiftAlreadyClosed,
    InvalidClosingAmount,
    UnexpectedError
}