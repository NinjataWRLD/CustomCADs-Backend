using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Shared.ApplicationEvents.Catalog;

public sealed record UserPurchasedProductApplicationEvent(
	ProductId[] Ids
) : BaseApplicationEvent;
