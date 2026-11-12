namespace PetStock.API.Customers.Application.Errors;

/// <summary>Possible errors when creating a customer.</summary>
public enum CreateCustomerError
{
    DuplicateCustomerEmail,
    UnexpectedError
}