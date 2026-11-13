namespace PetStock.API.Purchasing.Application.Errors;

/// <summary>Possible errors when creating a purchase order.</summary>
public enum CreatePurchaseOrderError
{
    InvalidPurchaseData,
    UnexpectedError
}