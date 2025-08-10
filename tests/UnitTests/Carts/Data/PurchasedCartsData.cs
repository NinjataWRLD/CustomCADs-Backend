using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Data;

using static PurchasedCartConstants.PurchasedCartItems;

public static class PurchasedCartsData
{
	public static class CartItemsData
	{
		public const int MinValidQuantity = QuantityMin + 1;
		public const int MaxValidQuantity = QuantityMax - 1;
		public const int MinInvalidQuantity = QuantityMin - 1;
		public const int MaxInvalidQuantity = QuantityMax + 1;

		public const decimal MinValidPrice = PriceMin + 1;
		public const decimal MaxValidPrice = PriceMax - 1;
		public const decimal MinInvalidPrice = PriceMin - 1;
		public const decimal MaxInvalidPrice = PriceMax + 1;

		public static readonly ProductId ValidProductId = ProductId.New();
		public static readonly CadId ValidCadId = CadId.New();
		public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
	}

	public static readonly PurchasedCartId ValidId = PurchasedCartId.New();
	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly ShipmentId ValidShipmentId = ShipmentId.New();
}
