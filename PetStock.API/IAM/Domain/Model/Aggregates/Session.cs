using PetStock.API.IAM.Domain.Model.Commands;

namespace PetStock.API.IAM.Domain.Model.Aggregates;

/// <summary>Active session of a user.</summary>
public partial class Session
{
    // Constructor for EF Core
    protected Session()
    {
        Token = string.Empty;
    }

    /// <summary>Creates a session from the command.</summary>
    public Session(CreateSessionCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        UserId = command.UserId;
        Token = command.Token;
        ExpiresAt = command.ExpiresAt;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Token { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
}