namespace PetStock.API.Analytics.Interfaces.REST.Resources;

/// <summary>
/// Report returned by the API.
/// </summary>
public record ReportResource(int Id, int UserId, string ReportType,
    string FileFormat, DateTimeOffset GeneratedAt);