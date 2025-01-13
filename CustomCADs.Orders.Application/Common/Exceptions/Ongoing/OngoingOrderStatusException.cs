using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderStatusException : BaseException
{
    private OngoingOrderStatusException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderStatusException General(Exception? inner = null)
        => new("Cannot perform the requested action for this Ongoing Order as it isn't Finished.", inner);

    public static OngoingOrderStatusException NotFinished(OngoingOrderId id, Exception? inner = null)
        => new($"Cannot perform the requested action for Ongoing Order: {id} as it isn't Finished.", inner);

    public static OngoingOrderStatusException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
