using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Inventory.Domain.Model.Aggregates;

/// <summary>Audit columns of ProductStock.</summary>
public partial class ProductStock : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
