namespace PetStock.API.Catalog.Application.Errors;

/// <summary>Possible errors when creating a product.</summary>
public enum CreateProductError
{
    InvalidProductData,
    CategoryNotFound,
    SupplierNotFound,
    UnexpectedError
}