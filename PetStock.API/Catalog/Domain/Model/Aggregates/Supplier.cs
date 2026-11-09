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
    /// <summary>Creates a supplier from the command. The company name cannot be empty.</summary>
    public Supplier(CreateSupplierCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (string.IsNullOrWhiteSpace(command.CompanyName))
            throw new ArgumentException("Supplier company name cannot be empty.", nameof(command));

        CompanyName = command.CompanyName.Trim();
        ContactName = command.ContactName.Trim();
        Phone = command.Phone.Trim();
        Email = command.Email.Trim();
    }

    public int Id { get; private set; }
    public string CompanyName { get; private set; }
    public string ContactName { get; private set; }

    /// <summary>Phone used for WhatsApp orders.</summary>
    public string Phone { get; private set; }

    public string Email { get; private set; }
}
