using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Application;

public class CustomNotFoundException<TEntity> : BaseException where TEntity : class
{
    private CustomNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CustomNotFoundException<TEntity> ById<TId>(
        TId id,
        Exception? inner = default
    ) where TId : struct
        => new($"The {typeof(TEntity).Name} with Id: {id} does not exist.", inner);

    public static CustomNotFoundException<TEntity> ById<TId>(
        TId id,
        string entity,
        Exception? inner = default
    ) where TId : struct
        => new($"The {entity} with Id: {id} does not exist.", inner);

    public static CustomNotFoundException<TEntity> ByProp<TProperty>(
        string name,
        TProperty property,
        Exception? inner = default
    )
        => new($"The {typeof(TEntity).Name} with {name}: {property} does not exist.", inner);

    public static CustomNotFoundException<TEntity> Custom(string message, Exception? inner = default)
        => new(message, inner);
}
