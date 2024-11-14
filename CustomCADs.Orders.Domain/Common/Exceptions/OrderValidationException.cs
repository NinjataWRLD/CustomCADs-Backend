using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions;

public class OrderValidationException : BaseException
{
    private OrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static OrderValidationException General(Exception? inner = default)
        => new("There was a validation error while working with an Order.", inner);

    public static OrderValidationException NotNull(string property, Exception? inner = default)
        => new($"An Order's {property} must not be null.", inner);

    public static OrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"An Order's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static OrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
