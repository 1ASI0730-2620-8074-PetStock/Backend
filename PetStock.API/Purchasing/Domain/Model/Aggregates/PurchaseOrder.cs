using PetStock.API.Purchasing.Domain.Model.Commands;
using PetStock.API.Purchasing.Domain.Model.Entities;

namespace PetStock.API.Purchasing.Domain.Model.Aggregates;

/// <summary>Order sent to a supplier. Starts as Pending and ends as Received.</summary>
public partial class PurchaseOrder
{
    // Constructor for EF Core
    protected PurchaseOrder()
    {
        Status = string.Empty;
        Items = new List<PurchaseOrderItem>();
    }

    /// <summary>Creates a pending order from the command.</summary>
    public PurchaseOrder(CreatePurchaseOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        SupplierId = command.SupplierId;
        OrderDate = DateTimeOffset.UtcNow;
        Status = "Pending";
        Items = new List<PurchaseOrderItem>();
    }

    public int Id { get; private set; }

    /// <summary>Supplier id from the Catalog bounded context.</summary>
    public int SupplierId { get; private set; }

    public DateTimeOffset OrderDate { get; private set; }
    public string Status { get; private set; }
    public ICollection<PurchaseOrderItem> Items { get; private set; }

    /// <summary>Adds a product line to the order.</summary>
    public void AddItem(int productId, int quantity, decimal unitCost)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        Items.Add(new PurchaseOrderItem(productId, quantity, unitCost));
    }

    /// <summary>Marks the order as received.</summary>
    public void MarkAsReceived()
    {
        Status = "Received";
    }
}
