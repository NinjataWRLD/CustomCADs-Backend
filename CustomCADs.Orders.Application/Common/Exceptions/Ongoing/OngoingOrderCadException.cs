using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderCadException : BaseException
{
    private OngoingOrderCadException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderCadException General(Exception? inner = null)
        => new("Cannot access this Ongoing Order's Cad as it has no CadId.", inner);

    public static OngoingOrderCadException ById(OngoingOrderId id, Exception? inner = null)
        => new($"Cannot access Ongoing Order: {id}'s Cad as it has no CadId.", inner);

    public static OngoingOrderCadException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
