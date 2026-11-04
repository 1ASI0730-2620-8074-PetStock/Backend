using PetStock.API.Sales.Domain.Model.Commands;

namespace PetStock.API.Sales.Domain.Model.Aggregates;

/// <summary>Payment made for a sale.</summary>
public partial class Payment
{
    // Constructor for EF Core
    protected Payment()
    {
        PaymentMethod = string.Empty;
    }

    /// <summary>Creates a payment from the command.</summary>
    public Payment(CreatePaymentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        SaleId = command.SaleId;
        Amount = command.Amount;
        PaymentMethod = command.PaymentMethod;
        PaidAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int SaleId { get; private set; }
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public DateTimeOffset PaidAt { get; private set; }
}
