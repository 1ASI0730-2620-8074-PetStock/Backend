namespace PetStock.API.IAM.Domain.Model.Commands;

/// <summary>Data needed to open a session after signing in.</summary>
public record CreateSessionCommand(int UserId, string Token, DateTimeOffset ExpiresAt);