﻿using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions;

public class ProductStatusException : BaseException
{
    private ProductStatusException(string message, Exception? inner) : base(message, inner) { }

    public static ProductStatusException General(Exception? inner = default)
        => new("The provided Product cannot perform the requested action.", inner);

    public static ProductStatusException ById(ProductId id, string action, Exception? inner = default)
        => new($"The Product with id: {id} cannot perform the action: {action}.", inner);

    public static ProductStatusException Custom(string message, Exception? inner = default)
        => new(message, inner);
}