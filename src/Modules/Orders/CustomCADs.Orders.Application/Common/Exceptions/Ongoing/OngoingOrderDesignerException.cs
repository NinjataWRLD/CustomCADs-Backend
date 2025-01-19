using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

public class OngoingOrderDesignerException : BaseException
{
    private OngoingOrderDesignerException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderDesignerException General(Exception? inner = null)
        => new("Cannot access this Ongoing Order's Designer as it has no DesignerId.", inner);

    public static OngoingOrderDesignerException ById(OngoingOrderId id, Exception? inner = null)
        => new($"Cannot access Ongoing Order: {id}'s Designer as it has no DesignerId.", inner);

    public static OngoingOrderDesignerException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
