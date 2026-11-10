using PetStock.API.Inventory.Domain.Model.Commands;

namespace PetStock.API.Inventory.Domain.Model.Aggregates;

/// <summary>Alert raised when a product reaches its minimum stock.</summary>
public partial class StockAlert
{
    // Constructor for EF Core
    protected StockAlert()
    {
        AlertLevel = string.Empty;
    }

    /// <summary>Creates an active alert from the command. The level cannot be empty.</summary>
    public StockAlert(CreateStockAlertCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (string.IsNullOrWhiteSpace(command.AlertLevel))
            throw new ArgumentException("Alert level cannot be empty.", nameof(command));

        ProductId = command.ProductId;
        AlertLevel = command.AlertLevel.Trim();
        IsActive = true;
    }

    public int Id { get; private set; }
    public int ProductId { get; private set; }

    /// <summary>Low or Critical.</summary>
    public string AlertLevel { get; private set; }

    public bool IsActive { get; private set; }

    /// <summary>Closes the alert after restocking.</summary>
    public void Resolve()
    {
        IsActive = false;
    }
}
