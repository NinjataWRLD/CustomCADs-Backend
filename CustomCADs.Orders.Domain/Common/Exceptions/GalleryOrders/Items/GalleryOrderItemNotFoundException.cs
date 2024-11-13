using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;

public class GalleryOrderItemNotFoundException : BaseException
{
    private GalleryOrderItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderItemNotFoundException General(Exception? inner = default)
        => new("The requested Item does not exist.", inner);

    public static GalleryOrderItemNotFoundException ById(GalleryOrderItemId id, Exception? inner = default)
        => new($"The Item with id: {id} does not exist.", inner);

    public static GalleryOrderItemNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
