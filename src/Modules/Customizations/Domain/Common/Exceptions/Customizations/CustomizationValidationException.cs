using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Customizations.Domain.Common.Exceptions.Customizations;

using static Constants.ExceptionMessages;

public class CustomizationValidationException : BaseException
{
    private CustomizationValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CustomizationValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Customization"), inner);

    public static CustomizationValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Customization", property), inner);

    public static CustomizationValidationException Min<TType>(string property, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "A", "Customization", property, min), inner);
    
    public static CustomizationValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "A", "Customization", property, min, max), inner);

    public static CustomizationValidationException Color(Exception? inner = default)
        => new("A Customization must have a proper color.", inner);

    public static CustomizationValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
