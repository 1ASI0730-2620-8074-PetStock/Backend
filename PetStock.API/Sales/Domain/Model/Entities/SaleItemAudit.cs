using PetStock.API.Shared.Domain.Model;

namespace PetStock.API.Sales.Domain.Model.Entities;

/// <summary>Audit columns of SaleItem.</summary>
public partial class SaleItem : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
