using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;

public class GalleryOrderValidationException : BaseException
{
    private GalleryOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Cart.", inner);

    public static GalleryOrderValidationException Range(string property, int max, int min, Exception? inner = default)
        => new($"A Cart's {property} must be less than {min} and more than {max}.", inner);

    public static GalleryOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
