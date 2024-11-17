﻿using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;

using static Constants.ExceptionMessages;

public class GalleryOrderNotFoundException : BaseException
{
    private GalleryOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static GalleryOrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Gallery Order"), inner);

    public static GalleryOrderNotFoundException ById(GalleryOrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Gallery Order", nameof(id), id), inner);

    public static GalleryOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
