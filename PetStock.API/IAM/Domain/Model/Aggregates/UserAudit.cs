using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.IAM.Domain.Model.Aggregates;

/// <summary>Audit columns of User (created_at, updated_at).</summary>
public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}