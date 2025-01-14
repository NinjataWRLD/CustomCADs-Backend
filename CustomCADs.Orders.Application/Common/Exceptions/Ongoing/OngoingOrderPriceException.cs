using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderPriceException : BaseException
{
    private OngoingOrderPriceException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderPriceException General(Exception? inner = null)
        => new("There was an error related to an Ongoing Order's Price.", inner);

    public static OngoingOrderPriceException ById(OngoingOrderId id, Exception? inner = null)
        => new($"Cannot perform this action as Ongoing Order: {id}'s Price is null.", inner);

    public static OngoingOrderPriceException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
