using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ActiveCartItemNotFoundException : BaseException
{
    private ActiveCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Cart"), inner);

    public static ActiveCartItemNotFoundException ById(ActiveCartItemId id, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart", nameof(id), id), inner);

    public static ActiveCartItemNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
