using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Purchasing.Domain.Model.Entities;

/// <summary>Audit columns of PurchaseOrderItem.</summary>
public partial class PurchaseOrderItem : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
