using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ActiveCartNotFoundException : BaseException
{
    private ActiveCartNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Cart"), inner);

    public static ActiveCartNotFoundException ByBuyerId(AccountId buyerId, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cart", nameof(buyerId), buyerId), inner);
    
    public static ActiveCartNotFoundException BuyerId(AccountId buyerId, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(buyerId), buyerId), inner);

    public static ActiveCartNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
