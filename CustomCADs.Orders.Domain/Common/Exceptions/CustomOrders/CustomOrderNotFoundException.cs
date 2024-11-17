using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

using static Constants.ExceptionMessages;

public class CustomOrderNotFoundException : BaseException
{
    private CustomOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CustomOrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Custom Order"), inner);

    public static CustomOrderNotFoundException ById(CustomOrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Custom Order", nameof(id), id), inner);

    public static CustomOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
