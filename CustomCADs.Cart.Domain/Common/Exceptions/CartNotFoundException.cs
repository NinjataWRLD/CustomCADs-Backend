using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Cart.Domain.Common.Exceptions;

public class CartNotFoundException : BaseException
{
    private CartNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CartNotFoundException General(Exception? inner = default)
        => new("The requested Cart does not exist.", inner);

    public static CartNotFoundException ById(CartId id, Exception? inner = default)
        => new($"The Cart with id: {id} does not exist.", inner);

    public static CartNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
