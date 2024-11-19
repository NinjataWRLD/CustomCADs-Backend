﻿namespace CustomCADs.Orders.Domain.Carts.Validation;

public static class CartConstants
{
    public const int ItemsCountMax = 10;
    public const int ItemsCountMin = 0;

    public static class GalleryOrders
    {
        public const int QuantityMax = 100;
        public const int QuantityMin = 1;

        public const decimal PriceMax = 1_000_000m;
        public const decimal PriceMin = 0.00_000_1m;
    }
}
