using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.CashShifts.Domain.Model.Aggregates;

/// <summary>Audit columns of CashReconciliation.</summary>
public partial class CashReconciliation : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
