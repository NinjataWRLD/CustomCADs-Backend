using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Exceptions;

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
