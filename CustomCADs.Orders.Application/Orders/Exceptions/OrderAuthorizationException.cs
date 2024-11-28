using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Orders.Exceptions;

public class OrderAuthorizationException : BaseException
{
    private OrderAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static OrderAuthorizationException General(Exception? inner = null)
        => new("Cannot modify another Buyer's Orders.", inner);
    
    public static OrderAuthorizationException ByOrderId(OrderId id, Exception? inner = null)
        => new($"Cannot modify another Buyer's Order: {id}.", inner);

    public static OrderAuthorizationException CannotViewNonPendingOrderNotAcceptedByYou(Exception? inner = default)
        => new("Cannot view an Order that isn't pending or accepted by you.", inner);

    public static OrderAuthorizationException NotAssociated(string action, Exception? inner = default)
        => new($"Cannot {action} an order you aren't associated with.", inner);

    public static OrderAuthorizationException UnauthorizedOrderRemoval(Exception? inner = default)
        => new("A User cannot remove an order if he isn't an Admin.", inner);

    public static OrderAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
