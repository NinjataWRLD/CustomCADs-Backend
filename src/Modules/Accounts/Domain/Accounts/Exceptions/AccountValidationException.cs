using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Accounts.Domain.Accounts.Exceptions;

using static Constants.ExceptionMessages;

public class AccountValidationException : BaseException
{
    private AccountValidationException(string message, Exception? inner) : base(message, inner) { }

    public static AccountValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Account"), inner);

    public static AccountValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "An", "Account", property), inner);

    public static AccountValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "An", "Account", property, min, max), inner);

    public static AccountValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static AccountValidationException Email(Exception? inner = default)
        => Custom("An Account must have a proper email.", inner);
}
