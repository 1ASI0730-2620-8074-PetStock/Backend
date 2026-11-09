namespace PetStock.API.Inventory.Application.Errors;

/// <summary>Possible errors when registering the stock of a product.</summary>
public enum CreateProductStockError
{
    InvalidStockQuantities,
    DuplicateProductStock,
    UnexpectedError
}