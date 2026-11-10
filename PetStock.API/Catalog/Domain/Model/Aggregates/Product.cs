using PetStock.API.Catalog.Domain.Model.Commands;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Product sold by the store. Its stock is handled by Inventory.</summary>
public partial class Product
{
    // Constructor for EF Core
    protected Product()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    /// <summary>Creates an active product from the command. The price cannot be negative.</summary>
    public Product(CreateProductCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (command.BasePrice < 0)
            throw new ArgumentException("Base price cannot be negative.", nameof(command));

        Name = command.Name;
        Description = command.Description;
        BasePrice = command.BasePrice;
        CategoryId = command.CategoryId;
        SupplierId = command.SupplierId;
        IsActive = true;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public int CategoryId { get; private set; }
    public int SupplierId { get; private set; }

    /// <summary>False when the product can no longer be sold.</summary>
    public bool IsActive { get; private set; }

    /// <summary>Stops selling the product without deleting it.</summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}
