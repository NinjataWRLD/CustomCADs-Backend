using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Data;

using static ActiveCartItemConstants;

public static class ActiveCartsData
{
	public const int ValidQuantity1 = QuantityMin + 1;
	public const int ValidQuantity2 = QuantityMax - 1;
	public const int InvalidQuantity = QuantityMax + 1;

	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly ProductId ValidProductId = ProductId.New();
	public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
}
