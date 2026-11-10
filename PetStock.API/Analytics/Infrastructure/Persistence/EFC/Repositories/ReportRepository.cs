using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.Analytics.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// EF Core repository for reports.
/// </summary>
public class ReportRepository(AppDbContext context)
    : BaseRepository<Report>(context),
        IReportRepository
{
}g