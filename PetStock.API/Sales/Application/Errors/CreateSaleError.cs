namespace PetStock.API.Sales.Application.Errors;

/// <summary>Possible errors when creating a sale.</summary>
public enum CreateSaleError
{
    InvalidSaleItems,
    UnexpectedError
}