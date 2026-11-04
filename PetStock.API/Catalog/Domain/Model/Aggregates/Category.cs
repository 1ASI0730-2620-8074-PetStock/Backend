using PetStock.API.Catalog.Domain.Model.Commands;

namespace PetStock.API.Catalog.Domain.Model.Aggregates;

/// <summary>Product category, for example food or accessories. The name is unique.</summary>
public partial class Category
{
    // Constructor for EF Core
    protected Category()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    /// <summary>Creates a category from the command. The name cannot be empty.</summary>
    public Category(CreateCategoryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException("Category name cannot be empty.", nameof(command));

        Name = command.Name.Trim();
        Description = command.Description.Trim();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
}
