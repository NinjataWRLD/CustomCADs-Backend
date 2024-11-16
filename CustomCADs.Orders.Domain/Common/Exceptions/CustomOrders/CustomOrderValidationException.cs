using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

public class CustomOrderValidationException : BaseException
{
    private CustomOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CustomOrderValidationException General(Exception? inner = default)
        => new("There was a validation error while working with an Order.", inner);

    public static CustomOrderValidationException NotNull(string property, Exception? inner = default)
        => new($"An Order's {property} must not be null.", inner);

    public static CustomOrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"An Order's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static CustomOrderValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => new("Cannot set a CadId for a Custom Order with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static CustomOrderValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => new("Cannot set a ShipmentId for a Custom Order with a DeliveryType that doesn't include a Physical Delivery.", inner);

    public static CustomOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
