using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Analytics.Domain.Model.Aggregates;

/// <summary>Audit columns of Report.</summary>
public partial class Report : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
