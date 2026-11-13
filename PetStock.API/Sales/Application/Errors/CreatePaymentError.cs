namespace PetStock.API.Sales.Application.Errors;

/// <summary>Possible errors when registering a payment.</summary>
public enum CreatePaymentError
{
    SaleNotFound,
    UnexpectedError
}
