using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Customizations.Domain.Common.Exceptions.Materials;

using static Constants.ExceptionMessages;

public class MaterialValidationException : BaseException
{
    private MaterialValidationException(string message, Exception? inner) : base(message, inner) { }

    public static MaterialValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Material"), inner);

    public static MaterialValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Material", property), inner);

    public static MaterialValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "Material", property, min, max), inner);

    public static MaterialValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "A", "Material", property, min, max), inner);

    public static MaterialValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
