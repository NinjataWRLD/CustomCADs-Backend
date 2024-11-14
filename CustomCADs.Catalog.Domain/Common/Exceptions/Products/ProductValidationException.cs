using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions.Products;

public class ProductValidationException : BaseException
{
    private ProductValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ProductValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Product.", inner);

    public static ProductValidationException NotNull(string property, Exception? inner = default)
        => new($"A Product's {property} must not be null.", inner);

    public static ProductValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"A Product's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static ProductValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new($"A Product's {property} must be less than {min} and more than {max}.", inner);

    public static ProductValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
