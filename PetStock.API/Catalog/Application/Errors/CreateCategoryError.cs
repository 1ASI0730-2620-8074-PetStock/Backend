namespace PetStock.API.Catalog.Application.Errors;

/// <summary>Possible errors when creating a category.</summary>
public enum CreateCategoryError
{
    InvalidCategoryName,
    DuplicateCategoryName,
    UnexpectedError
}
