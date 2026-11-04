namespace PetStock.API.Purchasing.Domain.Model.Commands;

/// <summary>Data needed to register the reception of an order.</summary>
public record CreateGoodsReceiptCommand(int PurchaseOrderId, string Notes);
