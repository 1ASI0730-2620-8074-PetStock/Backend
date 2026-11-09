using Microsoft.EntityFrameworkCore;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Catalog.Domain.Model.Commands;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Catalog.Application.Internal.CommandServices;

/// <summary>Creates suppliers.</summary>
public class SupplierCommandService(
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork,
    ILogger<SupplierCommandService> logger)
    : ISupplierCommandService
{
    /// <inheritdoc />
    public async Task<Result<Supplier, CreateSupplierError>> Handle(CreateSupplierCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var supplier = new Supplier(command);
            await supplierRepository.AddAsync(supplier, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Supplier, CreateSupplierError>.Success(supplier);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid arguments while creating supplier {CompanyName}", command.CompanyName);
            return new Result<Supplier, CreateSupplierError>.Failure(CreateSupplierError.InvalidSupplierData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating supplier {CompanyName}", command.CompanyName);
            return new Result<Supplier, CreateSupplierError>.Failure(CreateSupplierError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating supplier {CompanyName}", command.CompanyName);
            return new Result<Supplier, CreateSupplierError>.Failure(CreateSupplierError.UnexpectedError);
        }
    }
}