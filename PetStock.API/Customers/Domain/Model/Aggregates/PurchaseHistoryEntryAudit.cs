using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Customers.Domain.Model.Aggregates;

/// <summary>Audit columns of PurchaseHistoryEntry.</summary>
public partial class PurchaseHistoryEntry : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
