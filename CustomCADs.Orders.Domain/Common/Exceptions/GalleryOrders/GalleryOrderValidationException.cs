using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;

using static Constants.ExceptionMessages;

public class GalleryOrderValidationException : BaseException
{
    private GalleryOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Gallery Order"), inner);

    public static GalleryOrderValidationException Range(string property, int max, int min, Exception? inner = default)
        => new(string.Format(RangeValidation, "A", "Gallery Order", property, min, max), inner);

    public static GalleryOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
