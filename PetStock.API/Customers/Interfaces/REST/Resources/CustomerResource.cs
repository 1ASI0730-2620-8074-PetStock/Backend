namespace PetStock.API.Customers.Interfaces.REST.Resources;

/// <summary>Customer returned by the API.</summary>
public record CustomerResource(
    int Id,
    string Name,
    string Email,
    string Phone,
    string Address);