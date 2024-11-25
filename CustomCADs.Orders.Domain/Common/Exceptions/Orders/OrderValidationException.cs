using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Domain.Common.Exceptions.Orders;

using static Constants.ExceptionMessages;

public class OrderValidationException : BaseException
{
    private OrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static OrderValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Order"), inner);

    public static OrderValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "An", "Order", property), inner);

    public static OrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "An", "Order", property, min, max), inner);

    public static OrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static OrderValidationException InvalidStatus(OrderId id, string status, Exception? inner = default)
        => new($"The Order with id: {id} cannot have a status: {status}.", inner);

    public static OrderValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a CadId for a Order with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static OrderValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a ShipmentId for a Order with a DeliveryType that doesn't include a Physical Delivery.", inner);
}
