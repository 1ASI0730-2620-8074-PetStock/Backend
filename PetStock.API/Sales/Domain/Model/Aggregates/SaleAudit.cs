using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Sales.Domain.Model.Aggregates;

/// <summary>Audit columns of Sale.</summary>
public partial class Sale : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
