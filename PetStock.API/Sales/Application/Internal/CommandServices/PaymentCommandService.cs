using Microsoft.EntityFrameworkCore;
using PetStock.API.Sales.Application.Errors;
using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Model.Aggregates;
using PetStock.API.Sales.Domain.Model.Commands;
using PetStock.API.Sales.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Sales.Application.Internal.CommandServices;

/// <summary>Registers payments of existing sales.</summary>
public class PaymentCommandService(
    IPaymentRepository paymentRepository,
    ISaleRepository saleRepository,
    IUnitOfWork unitOfWork,
    ILogger<PaymentCommandService> logger)
    : IPaymentCommandService
{
    /// <inheritdoc />
    public async Task<Result<Payment, CreatePaymentError>> Handle(
        CreatePaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        // The payment must belong to an existing sale
        var sale = await saleRepository.FindByIdAsync(
            command.SaleId,
            cancellationToken);

        if (sale is null)
        {
            return new Result<Payment, CreatePaymentError>.Failure(
                CreatePaymentError.SaleNotFound);
        }

        try
        {
            var payment = new Payment(command);

            await paymentRepository.AddAsync(payment, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return new Result<Payment, CreatePaymentError>.Success(payment);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(
                ex,
                "Database update failed creating a payment for sale {SaleId}",
                command.SaleId);

            return new Result<Payment, CreatePaymentError>.Failure(
                CreatePaymentError.UnexpectedError);
        }
    }
}