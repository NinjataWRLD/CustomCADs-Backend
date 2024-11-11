using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Cart.Domain.Common.Exceptions;

public class ItemNotFoundException : BaseException
{
    private ItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ItemNotFoundException General(Exception? inner = default)
        => new("The requested Item does not exist.", inner);

    public static ItemNotFoundException ById(ItemId id, Exception? inner = default)
        => new($"The Item with id: {id} does not exist.", inner);

    public static ItemNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
