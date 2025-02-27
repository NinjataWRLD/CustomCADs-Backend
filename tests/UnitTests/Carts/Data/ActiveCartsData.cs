using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Data;

using static ActiveCartConstants;
using static ActiveCartConstants.ActiveCartItems;

public static class ActiveCartsData
{
    public static readonly ActiveCartId ValidId1 = ActiveCartId.New();
    public static readonly ActiveCartId ValidId2 = ActiveCartId.New();

    public const int ValidItemsCount1 = ItemsCountMin + 1;
    public const int ValidItemsCount2 = ItemsCountMax - 1;
    public const int InvalidItemsCount = ItemsCountMax + 1;

    public static readonly AccountId ValidBuyerId1 = AccountId.New();
    public static readonly AccountId ValidBuyerId2 = AccountId.New();

    public static class CartItemsData
    {
        public const int ValidQuantity1 = QuantityMin + 1;
        public const int ValidQuantity2 = QuantityMax - 1;
        public const int InvalidQuantity = QuantityMax + 1;

        public const double ValidWeight1 = WeightMin + 1;
        public const double ValidWeight2 = WeightMax - 1;
        public const double InvalidWeight1 = WeightMin - 1;
        public const double InvalidWeight2 = WeightMax + 1;

        public static readonly ProductId ValidProductId1 = ProductId.New();
        public static readonly ProductId ValidProductId2 = ProductId.New();
    }
}
