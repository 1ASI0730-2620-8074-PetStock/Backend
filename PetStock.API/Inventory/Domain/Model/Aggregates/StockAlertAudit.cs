using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Inventory.Domain.Model.Aggregates;

/// <summary>Audit columns of StockAlert.</summary>
public partial class StockAlert : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
