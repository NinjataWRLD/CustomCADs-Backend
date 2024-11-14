using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;

public class GalleryOrderItemValidationException : BaseException
{
    private GalleryOrderItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderItemValidationException General(Exception? inner = default)
        => new("There was a validation error while working with an Item.", inner);

    public static GalleryOrderItemValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new($"An Item's {property} must be less than {min} and more than {max}.", inner);

    public static GalleryOrderItemValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => new("Cannot set a CadId for a Gallery Order Item with a DeliveryType that doesn't include a Digital Delivery.", inner);
    
    public static GalleryOrderItemValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => new("Cannot set a ShipmentId for a Gallery Order Item with a DeliveryType that doesn't include a Physical Delivery.", inner);

    public static GalleryOrderItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}