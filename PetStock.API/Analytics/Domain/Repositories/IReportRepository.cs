using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Shared.Domain.Repositories;
namespace PetStock.API.Analytics.Domain.Repositories;

/// <summary>
/// Repository for reports.
/// </summary>
public interface IReportRepository
    : IBaseRepository<Report>
{
}