using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderAuthorizationException : BaseException
{
    private OngoingOrderAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderAuthorizationException General(Exception? inner = null)
        => new("Cannot access another Buyer's Ongoing Orders.", inner);

    public static OngoingOrderAuthorizationException ByOrderId(OngoingOrderId id, Exception? inner = null)
        => new($"Cannot access another Buyer's Ongoing Order: {id}.", inner);

    public static OngoingOrderAuthorizationException NotAssociated(OngoingOrderId id, string action, Exception? inner = default)
        => new($"Cannot {action} Ongoing Order: {id} as you aren't associated with it.", inner);

    public static OngoingOrderAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
