using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions;

public class CategoryValidationException : BaseException
{
    private CategoryValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CategoryValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Category.", inner);

    public static CategoryValidationException NotNull(string property, Exception? inner = default)
        => new($"A Category's {property} must not be null.", inner);

    public static CategoryValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"A Category's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static CategoryValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
