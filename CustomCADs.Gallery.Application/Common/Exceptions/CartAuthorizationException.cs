using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Gallery.Application.Common.Exceptions;

public class CartAuthorizationException : BaseException
{
    private CartAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static CartAuthorizationException General(Exception? inner = null)
        => new("Cannot modify another Buyer's Cart.", inner);

    public static CartAuthorizationException ByCartId(CartId id, Exception? inner = null)
        => new($"Cannot modify another Buyer's Cart: {id}.", inner);

    public static CartAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
