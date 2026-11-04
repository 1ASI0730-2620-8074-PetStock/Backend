using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Purchasing.Domain.Model.Aggregates;

/// <summary>Audit columns of GoodsReceipt.</summary>
public partial class GoodsReceipt : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
