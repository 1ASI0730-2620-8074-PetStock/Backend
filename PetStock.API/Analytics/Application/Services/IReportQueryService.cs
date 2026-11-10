using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Domain.Model.Queries;

namespace PetStock.API.Analytics.Application.Services;

/// <summary>
/// Handles report queries.
/// </summary>
public interface IReportQueryService
{
    Task<IEnumerable<Report>>
        Handle(
            GetAllReportsQuery query,
            CancellationToken cancellationToken = default);

    Task<Report?>
        Handle(
            GetReportByIdQuery query,
            CancellationToken cancellationToken = default);
}