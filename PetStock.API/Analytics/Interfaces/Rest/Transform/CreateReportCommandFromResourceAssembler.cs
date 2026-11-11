using PetStock.API.Analytics.Domain.Model.Commands;
using PetStock.API.Analytics.Interfaces.REST.Resources;

namespace PetStock.API.Analytics.Interfaces.REST.Transform;

/// <summary>
/// Converts a CreateReportResource into a CreateReportCommand.
/// </summary>
public static class CreateReportCommandFromResourceAssembler
{
    public static CreateReportCommand
        ToCommandFromResource(
            CreateReportResource resource)
    {
        return new CreateReportCommand(
            resource.UserId,
            resource.ReportType,
            resource.FileFormat);
    }
}