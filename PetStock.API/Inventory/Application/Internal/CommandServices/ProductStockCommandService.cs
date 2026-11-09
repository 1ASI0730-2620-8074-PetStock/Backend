using Microsoft.EntityFrameworkCore;
using PetStock.API.Inventory.Application.Errors;
using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Model.Aggregates;
using PetStock.API.Inventory.Domain.Model.Commands;
using PetStock.API.Inventory.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Inventory.Application.Internal.CommandServices;

/// <summary>Registers and updates product stocks. Each product has only one stock record.</summary>
public class ProductStockCommandService(
    IProductStockRepository productStockRepository,
    IUnitOfWork unitOfWork,
    ILogger<ProductStockCommandService> logger)
    : IProductStockCommandService
{
    /// <inheritdoc />
    public async Task<Result<ProductStock, CreateProductStockError>> Handle(CreateProductStockCommand command,
        CancellationToken cancellationToken = default)
    {
        // A product can only have one stock record
        if (await productStockRepository.ExistsByProductIdAsync(command.ProductId, cancellationToken))
        {
            logger.LogWarning("Duplicate stock rejected for product {ProductId}", command.ProductId);
            return new Result<ProductStock, CreateProductStockError>.Failure(
                CreateProductStockError.DuplicateProductStock);
        }

        try
        {
            var productStock = new ProductStock(command);
            await productStockRepository.AddAsync(productStock, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<ProductStock, CreateProductStockError>.Success(productStock);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid quantities while creating stock for product {ProductId}", command.ProductId);
            return new Result<ProductStock, CreateProductStockError>.Failure(
                CreateProductStockError.InvalidStockQuantities);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating stock for product {ProductId}", command.ProductId);
            return new Result<ProductStock, CreateProductStockError>.Failure(CreateProductStockError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating stock for product {ProductId}", command.ProductId);
            return new Result<ProductStock, CreateProductStockError>.Failure(CreateProductStockError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<ProductStock, UpdateProductStockError>> Handle(UpdateProductStockCommand command,
        CancellationToken cancellationToken = default)
    {
        var productStock = await productStockRepository.FindByIdAsync(command.Id, cancellationToken);
        if (productStock is null)
            return new Result<ProductStock, UpdateProductStockError>.Failure(
                UpdateProductStockError.ProductStockNotFound);

        try
        {
            productStock.UpdateStock(command);
            productStockRepository.Update(productStock);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<ProductStock, UpdateProductStockError>.Success(productStock);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid quantities while updating stock {ProductStockId}", command.Id);
            return new Result<ProductStock, UpdateProductStockError>.Failure(
                UpdateProductStockError.InvalidStockQuantities);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed updating stock {ProductStockId}", command.Id);
            return new Result<ProductStock, UpdateProductStockError>.Failure(UpdateProductStockError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error updating stock {ProductStockId}", command.Id);
            return new Result<ProductStock, UpdateProductStockError>.Failure(UpdateProductStockError.UnexpectedError);
        }
    }
}