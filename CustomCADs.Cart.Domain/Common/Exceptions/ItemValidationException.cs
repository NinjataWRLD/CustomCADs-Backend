using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Cart.Domain.Common.Exceptions;

public class ItemValidationException : BaseException
{
    private ItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ItemValidationException General(Exception? inner = default)
        => new("There was a validation error while working with an Item.", inner);

    public static ItemValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new($"An Item's {property} must be less than {min} and more than {max}.", inner);

    public static ItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}