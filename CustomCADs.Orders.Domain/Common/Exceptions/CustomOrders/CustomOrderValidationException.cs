using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

using static Constants.ExceptionMessages;

public class CustomOrderValidationException : BaseException
{
    private CustomOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CustomOrderValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Custom Order"), inner);

    public static CustomOrderValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Custom Order", property), inner);

    public static CustomOrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "Custom Order", property, min, max), inner);

    public static CustomOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static CustomOrderValidationException InvalidStatus(CustomOrderId id, string status, Exception? inner = default)
        => new($"The Custom Order with id: {id} cannot have a status: {status}.", inner);
    
    public static CustomOrderValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a CadId for a Custom Order with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static CustomOrderValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a ShipmentId for a Custom Order with a DeliveryType that doesn't include a Physical Delivery.", inner);
}
