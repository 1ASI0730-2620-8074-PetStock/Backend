using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Audit columns of Product.</summary>
public partial class Product : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
