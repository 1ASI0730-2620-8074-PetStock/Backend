namespace PetStock.API.Analytics.Domain.Model.Commands;

/// <summary>Data needed to generate a report (WeeklySales, TopProducts or LowMovement).</summary>
public record CreateReportCommand(int UserId, string ReportType, string FileFormat);
