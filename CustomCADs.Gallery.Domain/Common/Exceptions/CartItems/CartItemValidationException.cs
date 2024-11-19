using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;

using static Constants.ExceptionMessages;

public class CartItemValidationException : BaseException
{
    private CartItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CartItemValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Cart Item"), inner);

    public static CartItemValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "A", "Cart Item", property, min, max), inner);

    public static CartItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static CartItemValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a CadId for a Cart Item with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static CartItemValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a ShipmentId for a Cart Item with a DeliveryType that doesn't include a Physical Delivery.", inner);
}