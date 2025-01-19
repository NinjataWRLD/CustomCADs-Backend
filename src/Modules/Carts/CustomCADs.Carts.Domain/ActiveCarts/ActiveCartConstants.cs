namespace CustomCADs.Carts.Domain.ActiveCarts;

public static class ActiveCartConstants
{
    public const int BulkDeleteThreshold = 100;

    public const int ItemsCountMax = 10;
    public const int ItemsCountMin = 0;

    public static class ActiveCartItems
    {
        public const int QuantityMax = 100;
        public const int QuantityMin = 1;

        public const double WeightMax = 1000;
        public const double WeightMin = 0.01;
    }
}
