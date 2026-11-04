using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Audit columns of Supplier.</summary>
public partial class Supplier : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
