namespace CustomCADs.Gallery.Domain.Carts;

public static class CartConstants
{
    public const int ItemsCountMax = 10;
    public const int ItemsCountMin = 0;

    public static class CartItems
    {
        public const int QuantityMax = 100;
        public const int QuantityMin = 1;

        public const decimal PriceMax = 1_000_000m;
        public const decimal PriceMin = 0.00_000_1m;
    }
}
