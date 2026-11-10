namespace PetStock.API.CashShifts.Application.Errors;

public enum CreateCashReconciliationError
{
    CashShiftNotFound,
    DuplicateCashReconciliation,
    UnexpectedError
}