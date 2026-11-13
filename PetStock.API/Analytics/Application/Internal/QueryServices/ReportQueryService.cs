using PetStock.API.Analytics.Application.Services;
using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Domain.Model.Queries;
using PetStock.API.Analytics.Domain.Repositories;

namespace PetStock.API.Analytics.Application.Internal.QueryServices;

/// <summary>
/// Handles report queries.
/// </summary>
public class ReportQueryService(
    IReportRepository reportRepository)
    : IReportQueryService
{
    public async Task<IEnumerable<Report>>
        Handle(
            GetAllReportsQuery query,
            CancellationToken cancellationToken = default)
    {
        return await reportRepository.ListAsync(
            cancellationToken);
    }

    public async Task<Report?>
        Handle(
            GetReportByIdQuery query,
            CancellationToken cancellationToken = default)
    {
        return await reportRepository.FindByIdAsync(
            query.Id,
            cancellationToken);
    }
}