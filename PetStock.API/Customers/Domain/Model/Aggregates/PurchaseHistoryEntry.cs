using PetStock.API.Customers.Domain.Model.Commands;

namespace PetStock.API.Customers.Domain.Model.Aggregates;

/// <summary>One purchase in the history of a customer.</summary>
public partial class PurchaseHistoryEntry
{
    // Constructor for EF Core
    protected PurchaseHistoryEntry()
    {
    }

    /// <summary>Creates a history entry from the command.</summary>
    public PurchaseHistoryEntry(CreatePurchaseHistoryEntryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        CustomerId = command.CustomerId;
        SaleId = command.SaleId;
        TotalAmount = command.TotalAmount;
        PurchasedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int CustomerId { get; private set; }

    /// <summary>Sale id from the Sales bounded context.</summary>
    public int SaleId { get; private set; }

    public decimal TotalAmount { get; private set; }
    public DateTimeOffset PurchasedAt { get; private set; }
}
