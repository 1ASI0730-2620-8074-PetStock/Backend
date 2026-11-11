namespace PetStock.API.Purchasing.Domain.Model.Commands;

/// <summary>Data needed to delete a purchase order.</summary>
public record DeletePurchaseOrderCommand(int Id);