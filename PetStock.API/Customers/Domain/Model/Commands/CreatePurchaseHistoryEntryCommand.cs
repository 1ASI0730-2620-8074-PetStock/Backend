namespace PetStock.API.Customers.Domain.Model.Commands;

/// <summary>Data needed to save a purchase in the customer history.</summary>
public record CreatePurchaseHistoryEntryCommand(int CustomerId, int SaleId, decimal TotalAmount);
