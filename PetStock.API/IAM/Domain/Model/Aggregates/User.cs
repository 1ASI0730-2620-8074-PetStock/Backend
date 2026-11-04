using PetStock.API.IAM.Domain.Model.Commands;

namespace PetStock.API.IAM.Domain.Model.Aggregates;

/// <summary>PetStock user account (store owner or staff).</summary>
public partial class User
{
    // Constructor for EF Core
    protected User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Phone = string.Empty;
        Role = string.Empty;
    }

    /// <summary>Creates a user from the command.</summary>
    public User(CreateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        FirstName = command.FirstName;
        LastName = command.LastName;
        Email = command.Email;
        Password = command.Password;
        Phone = command.Phone;
        Role = command.Role;
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }

    /// <summary>Owner or Staff.</summary>
    public string Role { get; private set; }
}