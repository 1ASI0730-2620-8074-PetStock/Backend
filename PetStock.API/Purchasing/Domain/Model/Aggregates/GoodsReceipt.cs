using PetStock.API.Purchasing.Domain.Model.Commands;

namespace PetStock.API.Purchasing.Domain.Model.Aggregates;

/// <summary>Reception of the goods of a purchase order.</summary>
public partial class GoodsReceipt
{
    // Constructor for EF Core
    protected GoodsReceipt()
    {
        Notes = string.Empty;
    }

    /// <summary>Creates a goods receipt from the command.</summary>
    public GoodsReceipt(CreateGoodsReceiptCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        PurchaseOrderId = command.PurchaseOrderId;
        Notes = command.Notes;
        ReceivedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int PurchaseOrderId { get; private set; }
    public DateTimeOffset ReceivedAt { get; private set; }
    public string Notes { get; private set; }
}
