using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions;

public class OrderDeliveryException : BaseException
{
    private OrderDeliveryException(string message, Exception? inner) : base(message, inner) { }

    public static OrderDeliveryException General(Exception? inner = null)
        => new("There was a mismatch between the attempted action and this Order's Delivery flag.", inner);

    public static OrderDeliveryException ById(OrderId id, Exception? inner = null)
        => new($"There was a mismatch between the attempted action and Order: {id}'s Delivery flag.", inner);

    public static OrderDeliveryException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
