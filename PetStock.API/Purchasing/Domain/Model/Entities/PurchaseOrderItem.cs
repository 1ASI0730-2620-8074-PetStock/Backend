namespace PetStock.API.Purchasing.Domain.Model.Entities;

/// <summary>One line of a purchase order. Only created through PurchaseOrder.AddItem.</summary>
public partial class PurchaseOrderItem
{
    // Constructor for EF Core
    protected PurchaseOrderItem()
    {
    }

    /// <summary>Creates a purchase order line.</summary>
    public PurchaseOrderItem(int productId, int quantity, decimal unitCost)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitCost = unitCost;
    }

    public int Id { get; private set; }
    public int PurchaseOrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
}
