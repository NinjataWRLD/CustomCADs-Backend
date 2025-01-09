using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions.Completed;

using static Constants.ExceptionMessages;

public class CompletedOrderNotFoundException : BaseException
{
    private CompletedOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CompletedOrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Completed Order"), inner);

    public static CompletedOrderNotFoundException ById(CompletedOrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Completed Order", nameof(id), id), inner);

    public static CompletedOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
