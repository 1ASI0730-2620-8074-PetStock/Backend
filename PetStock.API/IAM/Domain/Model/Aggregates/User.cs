using PetStock.API.IAM.Domain.Model.Commands;

namespace PetStock.API.IAM.Domain.Model.Aggregates;

/// <summary>PetStock user account (store owner or staff).</summary>
public partial class User
{
    protected User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Phone = string.Empty;
        Role = string.Empty;
    }

    public User(CreateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (string.IsNullOrWhiteSpace(command.FirstName) || string.IsNullOrWhiteSpace(command.LastName))
            throw new ArgumentException("First name and last name cannot be empty.", nameof(command));
        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            throw new ArgumentException("Email and password cannot be empty.", nameof(command));

        FirstName = command.FirstName.Trim();
        LastName = command.LastName.Trim();
        Email = command.Email.Trim().ToLower();
        Password = command.Password;
        Phone = command.Phone.Trim();
        Role = command.Role.Trim();
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }
    public string Role { get; private set; }

    public void Update(UpdateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (string.IsNullOrWhiteSpace(command.FirstName) || string.IsNullOrWhiteSpace(command.LastName))
            throw new ArgumentException("First name and last name cannot be empty.", nameof(command));
        if (string.IsNullOrWhiteSpace(command.Email))
            throw new ArgumentException("Email cannot be empty.", nameof(command));

        FirstName = command.FirstName.Trim();
        LastName = command.LastName.Trim();
        Email = command.Email.Trim().ToLower();
        Phone = command.Phone.Trim();
    }
}
