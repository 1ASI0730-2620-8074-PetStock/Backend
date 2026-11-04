namespace PetStock.API.Sales.Domain.Model.Commands;

/// <summary>Data needed to register the payment of a sale.</summary>
public record CreatePaymentCommand(int SaleId, decimal Amount, string PaymentMethod);
