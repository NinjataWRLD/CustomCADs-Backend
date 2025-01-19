using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Common.Exceptions.Ongoing;

using static Constants.ExceptionMessages;

public class OngoingOrderNotFoundException : BaseException
{
    private OngoingOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static OngoingOrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Ongoing Order"), inner);

    public static OngoingOrderNotFoundException ById(OngoingOrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Ongoing Order", nameof(id), id), inner);

    public static OngoingOrderNotFoundException BuyerId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static OngoingOrderNotFoundException DesignerId(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static OngoingOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
