using PetStock.API.Catalog.Domain.Model.Commands;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Supplier that sells products to the store.</summary>
public partial class Supplier
{
    // Constructor for EF Core
    protected Supplier()
    {
        CompanyName = string.Empty;
        ContactName = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
    }

    /// <summary>Creates a supplier from the command.</summary>
    public Supplier(CreateSupplierCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        CompanyName = command.CompanyName;
        ContactName = command.ContactName;
        Phone = command.Phone;
        Email = command.Email;
    }

    public int Id { get; private set; }
    public string CompanyName { get; private set; }
    public string ContactName { get; private set; }

    /// <summary>Phone used for WhatsApp orders.</summary>
    public string Phone { get; private set; }

    public string Email { get; private set; }
}
