using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Application;

public class CustomStatusException<TEntity> : BaseException where TEntity : class
{
    private CustomStatusException(string message, Exception? inner) : base(message, inner) { }

    public static CustomStatusException<TEntity> ById<TId>(
        TId id,
        Exception? inner = default
    ) where TId : struct
        => new($"The {typeof(TEntity).Name} with id: {id} must have the proper status to execute this operation.", inner);

    public static CustomStatusException<TEntity> ById<TId>(
        TId id,
        string status,
        Exception? inner = default
    ) where TId : struct
        => new($"The {typeof(TEntity).Name} with id: {id} must have status: {status} to execute this operation.", inner);

    public static CustomStatusException<TEntity> Custom(string message, Exception? inner = default)
        => new(message, inner);
}
