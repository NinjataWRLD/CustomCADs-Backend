using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

using static Constants.ExceptionMessages;

public class OngoingOrderValidationException : BaseException
{
    private OngoingOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Ongoing Order"), inner);

    public static OngoingOrderValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "An", "Ongoing Order", property), inner);

    public static OngoingOrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "An", "Ongoing Order", property, min, max), inner);
    
    public static OngoingOrderValidationException Range(string property, decimal max, decimal min, Exception? inner = default)
        => new(string.Format(RangeValidation, "An", "Ongoing Order", property, min, max), inner);

    public static OngoingOrderValidationException InvalidStatus(OngoingOrderId id, OngoingOrderStatus oldStatus, OngoingOrderStatus newStatus, Exception? inner = default)
        => new($"Cannot set a status: {newStatus} to Ongoing Order with id: {id} and status: {oldStatus}.", inner);
    
    public static OngoingOrderValidationException CadIdOnOrderWithoutDesignerId(OngoingOrderId id, Exception? inner = default)
        => new($"Cannot set CadId on Ongoing Order: {id} as it has no DesignerId", inner);

    public static OngoingOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
