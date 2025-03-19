using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Files.Domain.Cads.Exceptions;

using static Constants.ExceptionMessages;

public class CadValidationException : BaseException
{
    private CadValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CadValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Cad"), inner);

    public static CadValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Cad", property), inner);

    public static CadValidationException Range(string property, decimal max, decimal min, Exception? inner = default)
        => new(string.Format(RangeValidation, "A", "Cad", property, min, max), inner);

    public static CadValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
