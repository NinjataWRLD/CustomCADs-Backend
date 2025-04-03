using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Domain;

public class CustomValidationException<TEntity> : BaseException
{
    private CustomValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CustomValidationException<TEntity> General(
        Exception? inner = default
    )
        => new($"There was a validation error while working with a/an ${typeof(TEntity).Name}.", inner);

    public static CustomValidationException<TEntity> NotNull(
        string property,
        Exception? inner = default
    )
        => new($"{typeof(TEntity).Name} requires property: ${property} to not be null.", inner);

    public static CustomValidationException<TEntity> Length(
        string property,
        int min,
        int max,
        Exception? inner = default
    )
        => new($"A/An {typeof(TEntity).Name}'s {property} length must be more than {min} and less than {max}.", inner);

    public static CustomValidationException<TEntity> Range<TRangeType>(
        string property,
        TRangeType min,
        TRangeType max,
        Exception? inner = default
    ) where TRangeType : struct
        => new($"A/An {typeof(TEntity).Name}'s {property} must be more than {min} and less than {max}.", inner);

    public static CustomValidationException<TEntity> Status<TStatus>(
        TStatus status,
        Exception? inner = default
    ) where TStatus : Enum
        => new($"Cannot edit this data on an {typeof(TEntity).Name} with status: {status}.", inner);

    public static CustomValidationException<TEntity> Status<TStatus>(
        TStatus newStatus,
        TStatus oldStatus,
        Exception? inner = default
    ) where TStatus : Enum
        => new($"Cannot set status: {newStatus} to {typeof(TEntity).Name} and status: {oldStatus}.", inner);

    public static CustomValidationException<TEntity> Custom(string message, Exception? inner = default)
        => new(message, inner);
}
