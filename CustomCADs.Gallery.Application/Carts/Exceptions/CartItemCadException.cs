using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Gallery.Application.Carts.Exceptions;

public class CartItemCadException : BaseException
{
    private CartItemCadException(string message, Exception? inner) : base(message, inner) { }

    public static CartItemCadException General(Exception? inner = null)
        => new("Cannot get this Item's Cad as it has no CadId.", inner);

    public static CartItemCadException ById(CartItemId id, Exception? inner = null)
        => new($"Cannot get Item: {id}'s Cad as it has no CadId.", inner);

    public static CartItemCadException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
