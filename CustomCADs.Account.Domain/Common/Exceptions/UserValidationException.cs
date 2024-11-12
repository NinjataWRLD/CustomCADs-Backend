using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Account.Domain.Common.Exceptions;

public class UserValidationException : BaseException
{
    private UserValidationException(string message, Exception? inner) : base(message, inner) { }

    public static UserValidationException General(Exception? inner = default)
        => new("There was a validation error while working with an User.", inner);

    public static UserValidationException NotNull(string property, Exception? inner = default)
        => new($"An User must have a non-null {property}.", inner);

    public static UserValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"An User's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static UserValidationException Email(Exception? inner = default)
        => new("An User must have a proper email.", inner);

    public static UserValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
