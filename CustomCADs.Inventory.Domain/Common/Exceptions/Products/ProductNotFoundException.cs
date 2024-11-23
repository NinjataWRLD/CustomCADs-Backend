﻿using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Domain.Common.Exceptions.Products;

public class ProductNotFoundException : BaseException
{
    private ProductNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ProductNotFoundException General(Exception? inner = default)
        => new("The requested Product does not exist.", inner);

    public static ProductNotFoundException ById(ProductId id, Exception? inner = default)
        => new($"The Product with id: {id} does not exist.", inner);

    public static ProductNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}