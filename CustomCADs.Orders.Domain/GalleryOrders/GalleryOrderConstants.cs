namespace CustomCADs.Orders.Domain.GalleryOrders;

public static class GalleryOrderConstants
{
    public const int ItemsCountMax = 10;
    public const int ItemsCountMin = 1;

    public static class Items
    {
        public const int QuantityMax = 100;
        public const int QuantityMin = 1;

        public const decimal PriceMax = 1_000_000m;
        public const decimal PriceMin = 0.00_000_1m;
    }
}
