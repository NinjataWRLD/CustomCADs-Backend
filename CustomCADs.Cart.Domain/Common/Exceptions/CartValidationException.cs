using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Cart.Domain.Common.Exceptions;

public class CartValidationException : BaseException
{
    private CartValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CartValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Cart.", inner);

    public static CartValidationException Range(string property, int max, int min, Exception? inner = default)
        => new($"A Cart's {property} must be less than {min} and more than {max}.", inner);

    public static CartValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
