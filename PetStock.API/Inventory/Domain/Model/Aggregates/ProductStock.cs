using PetStock.API.Inventory.Domain.Model.Commands;

namespace PetStock.API.Inventory.Domain.Model.Aggregates;

/// <summary>Stock of a product (one record per product).</summary>
public partial class ProductStock
{
    // Constructor for EF Core
    protected ProductStock()
    {
    }

    /// <summary>Creates the stock from the command. Quantities cannot be negative.</summary>
    public ProductStock(CreateProductStockCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (command.CurrentStock < 0 || command.MinimumStock < 0)
            throw new ArgumentException("Stock quantities cannot be negative.", nameof(command));

        ProductId = command.ProductId;
        CurrentStock = command.CurrentStock;
        MinimumStock = command.MinimumStock;
    }

    public int Id { get; private set; }

    /// <summary>Product id from the Catalog bounded context.</summary>
    public int ProductId { get; private set; }

    public int CurrentStock { get; private set; }

    /// <summary>Below or at this quantity the stock is low.</summary>
    public int MinimumStock { get; private set; }

    /// <summary>True when the stock is at or below the minimum.</summary>
    public bool IsLowStock => CurrentStock <= MinimumStock;

    /// <summary>Takes units out after a sale.</summary>
    public void Decrease(int quantity)
    {
        if (quantity > CurrentStock)
            throw new InvalidOperationException("Not enough stock for this product.");
        CurrentStock -= quantity;
    }

    /// <summary>Adds units after receiving goods.</summary>
    public void Increase(int quantity)
    {
        CurrentStock += quantity;
    }

    /// <summary>Replaces the current and minimum stock. Quantities cannot be negative.</summary>
    public void UpdateStock(UpdateProductStockCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (command.CurrentStock < 0 || command.MinimumStock < 0)
            throw new ArgumentException("Stock quantities cannot be negative.", nameof(command));

        CurrentStock = command.CurrentStock;
        MinimumStock = command.MinimumStock;
    }
}
