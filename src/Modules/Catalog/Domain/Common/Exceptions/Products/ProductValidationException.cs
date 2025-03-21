﻿using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions.Products;

using static Constants.ExceptionMessages;

public class ProductValidationException : BaseException
{
    private ProductValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ProductValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Product"), inner);

    public static ProductValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "a", "Product", property), inner);

    public static ProductValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "a", "Product", property, min, max), inner);

    public static ProductValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "a", "Product", property, min, max), inner);

    public static ProductValidationException Minimum<TType>(string property, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(MinimumValidation, "a", "Product", property, min), inner);

    public static ProductValidationException InvalidStatus(ProductId id, ProductStatus oldStatus, ProductStatus newStatus, Exception? inner = default)
        => new($"Cannot set a status: {newStatus} to Product with id: {id} and status: {oldStatus}.", inner);

    public static ProductValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
