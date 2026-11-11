using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Analytics.Interfaces.REST.Resources;

/// <summary>
/// Resource used to create a report.
/// </summary>
public record CreateReportResource(
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "ReportUserIdInvalid")]
    int UserId,

    [Required(
        ErrorMessage = "ReportTypeInvalid")]
    string ReportType,

    [Required(
        ErrorMessage = "ReportFileFormatInvalid")]
    string FileFormat);