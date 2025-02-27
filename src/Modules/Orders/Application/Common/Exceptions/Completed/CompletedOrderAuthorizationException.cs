using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Completed;

public class CompletedOrderAuthorizationException : BaseException
{
    private CompletedOrderAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static CompletedOrderAuthorizationException General(Exception? inner = null)
        => new("Cannot access another Buyer's Completed Orders.", inner);

    public static CompletedOrderAuthorizationException ByOrderId(CompletedOrderId id, Exception? inner = null)
        => new($"Cannot access another Buyer's Completed Order: {id}.", inner);

    public static CompletedOrderAuthorizationException NotAssociated(CompletedOrderId id, string action, Exception? inner = default)
        => new($"Cannot {action} a Completed Order: {id} as you aren't associated with it.", inner);

    public static CompletedOrderAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
