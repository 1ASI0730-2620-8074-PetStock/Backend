using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Purchasing.Domain.Model.Aggregates;

/// <summary>Audit columns of PurchaseOrder.</summary>
public partial class PurchaseOrder : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
