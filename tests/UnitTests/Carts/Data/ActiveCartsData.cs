using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Data;

using static ActiveCartItemConstants;

public static class ActiveCartsData
{
	public const int MinValidQuantity = QuantityMin + 1;
	public const int MaxValidQuantity = QuantityMax - 1;
	public const int MaxInvalidQuantity = QuantityMax + 1;
	public const int MinInvalidQuantity = QuantityMin - 1;

	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly ProductId ValidProductId = ProductId.New();
	public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
}
