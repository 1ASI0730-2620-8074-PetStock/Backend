using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Sales.Domain.Model.Aggregates;

/// <summary>Audit columns of Payment.</summary>
public partial class Payment : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
