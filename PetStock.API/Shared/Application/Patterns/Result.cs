namespace PetStock.API.Shared.Application.Patterns;

/// <summary>Result of an operation: a value when it succeeds or an error when it fails.</summary>
/// <typeparam name="TValue">Type of the value on success</typeparam>
/// <typeparam name="TError">Type of the error on failure</typeparam>
public abstract record Result<TValue, TError>
{
    /// <summary>Successful result with its value.</summary>
    public sealed record Success(TValue Value) : Result<TValue, TError>;

    /// <summary>Failed result with its error.</summary>
    public sealed record Failure(TError Error) : Result<TValue, TError>;

    /// <summary>True when the operation succeeded.</summary>
    public bool IsSuccess => this is Success;

    /// <summary>True when the operation failed.</summary>
    public bool IsFailure => this is Failure;

    /// <summary>Transforms the value if successful; keeps the error otherwise.</summary>
    public Result<TNext, TError> Map<TNext>(Func<TValue, TNext> onSuccess) =>
        this switch
        {
            Success s => new Result<TNext, TError>.Success(onSuccess(s.Value)),
            Failure f => new Result<TNext, TError>.Failure(f.Error),
            _ => throw new InvalidOperationException("Unknown Result type")
        };

    /// <summary>Returns a value for the success case or for the failure case.</summary>
    public TResult Fold<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onFailure) =>
        this switch
        {
            Success s => onSuccess(s.Value),
            Failure f => onFailure(f.Error),
            _ => throw new InvalidOperationException("Unknown Result type")
        };

    /// <summary>Runs an action for the success case or for the failure case.</summary>
    public void Match(Action<TValue> onSuccess, Action<TError> onFailure)
    {
        if (this is Success s)
            onSuccess(s.Value);
        else if (this is Failure f)
            onFailure(f.Error);
    }
}