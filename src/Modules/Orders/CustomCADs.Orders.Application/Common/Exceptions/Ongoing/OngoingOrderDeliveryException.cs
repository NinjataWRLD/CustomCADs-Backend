using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderDeliveryException : BaseException
{
    private OngoingOrderDeliveryException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderDeliveryException General(Exception? inner = null)
        => new("There was a mismatch between the attempted action and this Order's Delivery flag.", inner);

    public static OngoingOrderDeliveryException ById(OngoingOrderId id, Exception? inner = null)
        => new($"There was a mismatch between the attempted action and Ongoing Order: {id}'s Delivery flag.", inner);

    public static OngoingOrderDeliveryException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
