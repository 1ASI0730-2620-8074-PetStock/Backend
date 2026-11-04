namespace PetStock.API.Shared.Domain.Model;

/// <summary>Entity with audit dates filled automatically when it is saved.</summary>
public interface IAuditableEntity
{
    /// <summary>Date when the entity was created.</summary>
    DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date when the entity was last updated.</summary>
    DateTimeOffset? UpdatedAt { get; set; }
}