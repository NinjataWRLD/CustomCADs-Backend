using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Account.Domain.Common.Exceptions;

using static Constants.ExceptionMessages;

public class RoleValidationException : BaseException
{
    private RoleValidationException(string message, Exception? inner) : base(message, inner) { }

    public static RoleValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Role"), inner);

    public static RoleValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Role", property), inner);

    public static RoleValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "Role", property, min, max), inner);

    public static RoleValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
