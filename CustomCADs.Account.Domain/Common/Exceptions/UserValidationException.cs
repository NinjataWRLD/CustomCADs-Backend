using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Account.Domain.Common.Exceptions;

using static Constants.ExceptionMessages;

public class UserValidationException : BaseException
{
    private UserValidationException(string message, Exception? inner) : base(message, inner) { }

    public static UserValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "User"), inner);

    public static UserValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "User", property), inner);

    public static UserValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "User", property, min, max), inner);

    public static UserValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static UserValidationException Email(Exception? inner = default)
        => Custom("An User must have a proper email.", inner);
}
