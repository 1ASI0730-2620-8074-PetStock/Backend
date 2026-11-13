namespace PetStock.API.Customers.Application.Errors;

/// <summary>Possible errors when saving a purchase in the customer history.</summary>
public enum CreatePurchaseHistoryEntryError
{
    CustomerNotFound,
    UnexpectedError
}