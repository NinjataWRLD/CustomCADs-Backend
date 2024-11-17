using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;

using static Constants.ExceptionMessages;

public class GalleryOrderItemNotFoundException : BaseException
{
    private GalleryOrderItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderItemNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Gallery Order Item"), inner);
    public static GalleryOrderItemNotFoundException ById(GalleryOrderItemId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Gallery Order Item", nameof(id), id), inner);

    public static GalleryOrderItemNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
