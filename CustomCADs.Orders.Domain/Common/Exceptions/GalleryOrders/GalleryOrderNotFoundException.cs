using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;

public class GalleryOrderNotFoundException : BaseException
{
    private GalleryOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderNotFoundException General(Exception? inner = default)
        => new("The requested Cart does not exist.", inner);

    public static GalleryOrderNotFoundException ById(GalleryOrderId id, Exception? inner = default)
        => new($"The Cart with id: {id} does not exist.", inner);

    public static GalleryOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
