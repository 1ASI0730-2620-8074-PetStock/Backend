using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Customers.Domain.Model.Aggregates;

/// <summary>Audit columns of Customer.</summary>
public partial class Customer : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
