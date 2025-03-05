using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Data;

using static PurchasedCartConstants;
using static PurchasedCartConstants.PurchasedCartItems;

public static class PurchasedCartsData
{
    public static readonly PurchasedCartId ValidId1 = PurchasedCartId.New();
    public static readonly PurchasedCartId ValidId2 = PurchasedCartId.New();

    public const int ValidItemsCount1 = ItemsCountMin + 1;
    public const int ValidItemsCount2 = ItemsCountMax - 1;
    public const int InvalidItemsCount = ItemsCountMax + 1;

    public static readonly AccountId ValidBuyerId1 = AccountId.New();
    public static readonly AccountId ValidBuyerId2 = AccountId.New();

    public static readonly ShipmentId ValidShipmentId1 = ShipmentId.New();
    public static readonly ShipmentId? ValidShipmentId2 = null;

    public static class CartItemsData
    {
        public const int ValidQuantity1 = QuantityMin + 1;
        public const int ValidQuantity2 = QuantityMax - 1;
        public const int InvalidQuantity1 = QuantityMin - 1;
        public const int InvalidQuantity2 = QuantityMax + 1;

        public const decimal ValidPrice1 = PriceMin + 1;
        public const decimal ValidPrice2 = PriceMax - 1;
        public const decimal InvalidPrice1 = PriceMin - 1;
        public const decimal InvalidPrice2 = PriceMax + 1;

        public static readonly ProductId ValidProductId1 = ProductId.New();
        public static readonly ProductId ValidProductId2 = ProductId.New();

        public static readonly CadId ValidCadId1 = CadId.New();
        public static readonly CadId ValidCadId2 = CadId.New();

        public static readonly CustomizationId ValidCustomizationId1 = CustomizationId.New();
        public static readonly CustomizationId ValidCustomizationId2 = CustomizationId.New();
    }
}
