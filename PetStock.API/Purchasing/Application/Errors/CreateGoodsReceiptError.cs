namespace PetStock.API.Purchasing.Application.Errors;

/// <summary>
/// Possible errors when creating a goods receipt.
/// </summary>
public enum CreateGoodsReceiptError
{
    PurchaseOrderNotFound,
    PurchaseOrderAlreadyReceived,
    UnexpectedError
}