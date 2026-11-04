using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Application.Services;

/// <summary>Write operations for categories.</summary>
public interface ICategoryCommandService
{
    /// <summary>Creates a category; returns the category or the error.</summary>
    Task<Result<Category, CreateCategoryError>> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken = default);
}
