using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

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

    public static OrderValidationException InvalidStatus(OrderId id, OrderStatus oldStatus, OrderStatus newStatus, Exception? inner = default)
        => new($"Cannot set a status: {newStatus} to Product with id: {id} and status: {oldStatus}.", inner);

    public static OrderValidationException ShipmentIdOnNonDelivery(Exception? inner = default)
        => new("Cannot set a ShipmentId for an Order without Delivery.", inner);

    public static OrderValidationException CadIdOnNonFinished(Exception? inner = default)
        => new("Cannot set a CadId for an Order that isn't Finished.", inner);

    public static OrderValidationException ShipmentIdOnNonFinished(Exception? inner = default)
        => new("Cannot set a ShipemntId for an Order that isn't Finished.", inner);

    public static OrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
