namespace CustomCADs.Shared.Application.Events.Catalog;

public sealed record UserPurchasedProductApplicationEvent(
	ProductId[] Ids
) : BaseApplicationEvent;
