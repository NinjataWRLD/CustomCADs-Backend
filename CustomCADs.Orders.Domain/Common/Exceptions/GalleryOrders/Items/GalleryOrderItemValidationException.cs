using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;

using static Constants.ExceptionMessages;

public class GalleryOrderItemValidationException : BaseException
{
    private GalleryOrderItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderItemValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Gallery Order Order"), inner);

    public static GalleryOrderItemValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "A", "Gallery Order Item", property, min, max), inner);

    public static GalleryOrderItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static GalleryOrderItemValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a CadId for a Gallery Order Item with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static GalleryOrderItemValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a ShipmentId for a Gallery Order Item with a DeliveryType that doesn't include a Physical Delivery.", inner);
}