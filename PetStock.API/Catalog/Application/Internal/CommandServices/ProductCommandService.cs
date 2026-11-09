using Microsoft.EntityFrameworkCore;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.CommandServices;

/// <summary>Creates products after checking that their category and supplier exist.</summary>
public class ProductCommandService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork,
    ILogger<ProductCommandService> logger)
    : IProductCommandService
{
    /// <inheritdoc />
    public async Task<Result<Product, CreateProductError>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        // The category must exist
        if (await categoryRepository.FindByIdAsync(command.CategoryId, cancellationToken) is null)
        {
            logger.LogWarning("Product rejected because category {CategoryId} does not exist", command.CategoryId);
            return new Result<Product, CreateProductError>.Failure(CreateProductError.CategoryNotFound);
        }

        // The supplier must exist
        if (await supplierRepository.FindByIdAsync(command.SupplierId, cancellationToken) is null)
        {
            logger.LogWarning("Product rejected because supplier {SupplierId} does not exist", command.SupplierId);
            return new Result<Product, CreateProductError>.Failure(CreateProductError.SupplierNotFound);
        }

        try
        {
            var product = new Product(command);
            await productRepository.AddAsync(product, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Product, CreateProductError>.Success(product);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while creating product {ProductName}", command.Name);
            return new Result<Product, CreateProductError>.Failure(CreateProductError.InvalidProductData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating product {ProductName}", command.Name);
            return new Result<Product, CreateProductError>.Failure(CreateProductError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating product {ProductName}", command.Name);
            return new Result<Product, CreateProductError>.Failure(CreateProductError.UnexpectedError);
        }
    }
}