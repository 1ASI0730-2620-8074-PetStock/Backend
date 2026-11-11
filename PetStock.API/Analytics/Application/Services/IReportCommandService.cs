using PetStock.API.Analytics.Application.Errors;
using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Domain.Model.Commands;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Analytics.Application.Services;

/// <summary>
/// Handles report commands.
/// </summary>
public interface IReportCommandService
{
    Task<Result<Report, CreateReportError>>
        Handle(
            CreateReportCommand command,
            CancellationToken cancellationToken = default);
}