namespace PetStock.API.Sales.Domain.Model.Entities;

/// <summary>One line of a sale. Only created through Sale.AddItem.</summary>
public partial class SaleItem
{
    // Constructor for EF Core
    protected SaleItem()
    {
    }

    /// <summary>Creates a sale line.</summary>
    public SaleItem(int productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public int Id { get; private set; }
    public int SaleId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    /// <summary>Price of one unit at the moment of the sale.</summary>
    public decimal UnitPrice { get; private set; }
}
