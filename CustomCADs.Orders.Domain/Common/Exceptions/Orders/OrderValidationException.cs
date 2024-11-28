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

    public static OrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static OrderValidationException InvalidStatus(OrderId id, string status, Exception? inner = default)
        => new($"The Order with id: {id} cannot have a status: {status}.", inner);

    public static OrderValidationException CadIdOnNonDigitalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a CadId for a Order with a DeliveryType that doesn't include a Digital Delivery.", inner);

    public static OrderValidationException ShipmentIdOnNonPhysicalDeliveryType(Exception? inner = default)
        => Custom("Cannot set a ShipmentId for a Order with a DeliveryType that doesn't include a Physical Delivery.", inner);

    public static OrderValidationException Unauthorized(Exception? inner = default)
        => Custom("Cannot modify another Buyer's Orders.", inner);

    public static OrderValidationException DesignerNotAssociated(string action, Exception? inner = default)
        => Custom($"Cannot {action} an order you aren't associated with.", inner);

    public static OrderValidationException CannotSetCadIdOnNonFinishedOrder(Exception? inner = default)
        => Custom("Cannot set a CadId for an Order that isn't Finished.", inner);

    public static OrderValidationException CannotFinishOrderWithDigitalDeliveryWithoutCadId(Exception? inner = default)
        => Custom("Cannot finish a Digital delivery type Order without providing a CadId.", inner);

    public static OrderValidationException CannotViewNonPendingOrderNotAcceptedByYou(Exception? inner = default)
        => Custom("Cannot view an Order that isn't pending or accepted by you.", inner);

    public static OrderValidationException OnlyAdminsCanRemoveOrders(Exception? inner = default)
        => Custom("A User cannot remove an order if he isn't an Admin.", inner);

    public static OrderValidationException CannotGetOrderCadWithoutCadId(Exception? inner = default)
        => Custom("Cannot get an Order's Cad without a CadId.", inner);
}
