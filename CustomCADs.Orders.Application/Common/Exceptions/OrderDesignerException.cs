using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions;

public class OrderDesignerException : BaseException
{
    private OrderDesignerException(string message, Exception? inner) : base(message, inner) { }

    public static OrderDesignerException General(Exception? inner = null)
        => new("Cannot access this Order's Designer as it has no DesignerId.", inner);

    public static OrderDesignerException ById(OrderId id, Exception? inner = null)
        => new($"Cannot access Order: {id}'s Designer as it has no DesignerId.", inner);

    public static OrderDesignerException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
