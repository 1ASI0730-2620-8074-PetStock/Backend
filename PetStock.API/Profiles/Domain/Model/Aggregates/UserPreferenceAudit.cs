using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Profiles.Domain.Model.Aggregates;

/// <summary>Audit columns of UserPreference.</summary>
public partial class UserPreference : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
