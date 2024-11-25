﻿namespace CustomCADs.Inventory.Domain.Products.ValueObjects;

public record Image(string Key, string ContentType)
{
    public Image() : this(string.Empty, string.Empty) { }
}