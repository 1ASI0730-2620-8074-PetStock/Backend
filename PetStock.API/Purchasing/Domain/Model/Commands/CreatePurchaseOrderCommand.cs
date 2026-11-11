namespace PetStock.API.Purchasing.Domain.Model.Commands;

/// <summary>Data needed to create a purchase order for a supplier.</summary>
public record CreatePurchaseOrderCommand(
    int SupplierId,
    int ProductId,
    int Quantity,
    decimal UnitCost);