using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

public class PurchasedCartAuthorizationException : BaseException
{
    private PurchasedCartAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartAuthorizationException General(Exception? inner = null)
        => new("Cannot access another Buyer's Cart.", inner);

    public static PurchasedCartAuthorizationException ById(PurchasedCartId id, Exception? inner = null)
        => new($"Cannot access another Buyer's Cart: {id}.", inner);

    public static PurchasedCartAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
