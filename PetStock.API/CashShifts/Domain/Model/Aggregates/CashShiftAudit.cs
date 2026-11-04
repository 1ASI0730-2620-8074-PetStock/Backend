using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.CashShifts.Domain.Model.Aggregates;

/// <summary>Audit columns of CashShift.</summary>
public partial class CashShift : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
