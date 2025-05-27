using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Data;

using static ActiveCartItemConstants;

public static class ActiveCartsData
{
	public static readonly AccountId ValidBuyerId1 = AccountId.New();
	public static readonly AccountId ValidBuyerId2 = AccountId.New();

	public const int ValidQuantity1 = QuantityMin + 1;
	public const int ValidQuantity2 = QuantityMax - 1;
	public const int InvalidQuantity = QuantityMax + 1;

	public static readonly ProductId ValidProductId1 = ProductId.New();
	public static readonly ProductId ValidProductId2 = ProductId.New();

	public static readonly CustomizationId ValidCustomizationId1 = CustomizationId.New();
	public static readonly CustomizationId ValidCustomizationId2 = CustomizationId.New();
}
