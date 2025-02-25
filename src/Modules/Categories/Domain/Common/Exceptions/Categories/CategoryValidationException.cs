using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Categories.Domain.Common.Exceptions.Categories;

using static Constants.ExceptionMessages;

public class CategoryValidationException : BaseException
{
    private CategoryValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CategoryValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Category"), inner);

    public static CategoryValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Category", property), inner);

    public static CategoryValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "Category", property, min, max), inner);

    public static CategoryValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
