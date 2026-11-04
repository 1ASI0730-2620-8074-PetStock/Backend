using PetStock.API.Customers.Domain.Model.Commands;

namespace PetStock.API.Customers.Domain.Model.Aggregates;

/// <summary>Recurring customer of the store.</summary>
public partial class Customer
{
    // Constructor for EF Core
    protected Customer()
    {
        FullName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
    }

    /// <summary>Creates a customer from the command.</summary>
    public Customer(CreateCustomerCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        FullName = command.FullName;
        Email = command.Email;
        Phone = command.Phone;
        Address = command.Address;
    }

    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
}
