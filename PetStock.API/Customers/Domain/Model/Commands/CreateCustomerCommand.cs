namespace PetStock.API.Customers.Domain.Model.Commands;

/// <summary>Data needed to register a recurring customer.</summary>
public record CreateCustomerCommand(string FullName, string Email, string Phone, string Address);
