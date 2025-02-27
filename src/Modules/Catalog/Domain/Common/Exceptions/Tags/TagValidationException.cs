using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions.Tags;

using static Constants.ExceptionMessages;

public class TagValidationException : BaseException
{
    private TagValidationException(string message, Exception? inner) : base(message, inner) { }

    public static TagValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Tag"), inner);

    public static TagValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "a", "Tag", property), inner);

    public static TagValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "a", "Tag", property, min, max), inner);

    public static TagValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
