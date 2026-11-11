using PetStock.API.Analytics.Domain.Model.Aggregates;
using PetStock.API.Analytics.Interfaces.REST.Resources;

namespace PetStock.API.Analytics.Interfaces.REST.Transform;

/// <summary>
/// Converts a Report entity into a ReportResource.
/// </summary>
public static class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResourceFromEntity(Report entity)
    {
        return new ReportResource(entity.Id, entity.UserId,
            entity.ReportType, entity.FileFormat, entity.GeneratedAt);
    }
}