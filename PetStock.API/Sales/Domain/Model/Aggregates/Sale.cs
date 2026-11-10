using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Domain.Model.Entities;

namespace PetStock.API.Sales.Domain.Model.Aggregates;

/// <summary>Sale registered at the point of sale. Root of its items.</summary>
public partial class Sale
{
    // Constructor for EF Core
    protected Sale()
    {
        PaymentMethod = string.Empty;
        Items = new List<SaleItem>();
    }

    /// <summary>Creates an empty sale from the command.</summary>
    public Sale(CreateSaleCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        UserId = command.UserId;
        CustomerId = command.CustomerId;
        PaymentMethod = command.PaymentMethod;
        TotalAmount = 0;
        SaleDate = DateTimeOffset.UtcNow;
        Items = new List<SaleItem>();
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int? CustomerId { get; private set; }

    /// <summary>Cash, Card or Yape.</summary>
    public string PaymentMethod { get; private set; }

    public decimal TotalAmount { get; private set; }
    public DateTimeOffset SaleDate { get; private set; }
    public ICollection<SaleItem> Items { get; private set; }

    /// <summary>Adds a product line and updates the total.</summary>
    public void AddItem(int productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        Items.Add(new SaleItem(productId, quantity, unitPrice));
        TotalAmount += quantity * unitPrice;
    }
}
