using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Audit columns of Category.</summary>
public partial class Category : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
