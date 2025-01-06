using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Common.Exceptions;

public class ActiveCartAlreadyExistsException : BaseException
{
    private ActiveCartAlreadyExistsException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartAlreadyExistsException General(Exception? inner = null)
        => new("Active cart already exists.", inner);

    public static ActiveCartAlreadyExistsException ByBuyerId(AccountId buyerId, Exception? inner = null)
        => new($"Active cart for buyer: {buyerId} already exists.", inner);

    public static ActiveCartAlreadyExistsException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
