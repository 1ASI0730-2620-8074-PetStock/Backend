namespace PetStock.API.Inventory.Application.Errors;

/// <summary>Possible errors when updating the stock of a product.</summary>
public enum UpdateProductStockError
{
    ProductStockNotFound,
    InvalidStockQuantities,
    UnexpectedError
}