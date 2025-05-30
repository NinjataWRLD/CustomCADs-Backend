using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Data;

using static PurchasedCartConstants.PurchasedCartItems;

public static class PurchasedCartsData
{
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

		public static readonly ProductId ValidProductId = ProductId.New();
		public static readonly CadId ValidCadId = CadId.New();
		public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
	}

	public static readonly PurchasedCartId ValidId = PurchasedCartId.New();
	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly ShipmentId ValidShipmentId = ShipmentId.New();
}
