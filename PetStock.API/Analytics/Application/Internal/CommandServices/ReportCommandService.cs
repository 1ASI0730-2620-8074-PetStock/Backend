using PetStock.API.Analytics.Application.Errors;
using PetStock.API.Analytics.Application.Services;
using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Domain.Model.Commands;
using PetStock.API.Analytics.Domain.Repositories;
using PetStock.API.Shared.Application.Patterns;
using PetStock.API.Shared.Domain.Repositories;

namespace PetStock.API.Analytics.Application.Internal.CommandServices;

/// <summary>
/// Handles report creation commands.
/// </summary>
public class ReportCommandService(
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork,
    ILogger<ReportCommandService> logger)
    : IReportCommandService
{
    public async Task<Result<Report, CreateReportError>>
        Handle(
            CreateReportCommand command,
            CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.UserId <= 0 ||
                string.IsNullOrWhiteSpace(command.ReportType) ||
                string.IsNullOrWhiteSpace(command.FileFormat))
            {
                return new Result<
                    Report,
                    CreateReportError>.Failure(
                    CreateReportError.InvalidReportData);
            }

            var report = new Report(command);

            await reportRepository.AddAsync(
                report,
                cancellationToken);

            await unitOfWork.CompleteAsync(
                cancellationToken);

            return new Result<
                Report,
                CreateReportError>.Success(
                report);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Unexpected error creating report.");

            return new Result<
                Report,
                CreateReportError>.Failure(
                CreateReportError.UnexpectedError);
        }
    }
}