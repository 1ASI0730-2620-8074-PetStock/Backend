using Microsoft.EntityFrameworkCore;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.CommandServices;

/// <summary>Creates categories and rejects duplicated names.</summary>
public class CategoryCommandService(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ILogger<CategoryCommandService> logger)
    : ICategoryCommandService
{
    /// <inheritdoc />
    public async Task<Result<Category, CreateCategoryError>> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        // The name must be unique
        if (await categoryRepository.ExistsByNameAsync(command.Name.Trim(), cancellationToken))
        {
            logger.LogWarning("Duplicate category rejected for name {CategoryName}", command.Name);
            return new Result<Category, CreateCategoryError>.Failure(CreateCategoryError.DuplicateCategoryName);
        }

        try
        {
            var category = new Category(command);
            await categoryRepository.AddAsync(category, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Category, CreateCategoryError>.Success(category);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while creating category {CategoryName}", command.Name);
            return new Result<Category, CreateCategoryError>.Failure(CreateCategoryError.InvalidCategoryName);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating category {CategoryName}", command.Name);
            return new Result<Category, CreateCategoryError>.Failure(CreateCategoryError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating category {CategoryName}", command.Name);
            return new Result<Category, CreateCategoryError>.Failure(CreateCategoryError.UnexpectedError);
        }
    }
}
