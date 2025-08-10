namespace CustomCADs.Shared.Application.Events.Catalog;

public record UserViewedProductApplicationEvent(
	AccountId AccountId,
	ProductId Id
) : BaseApplicationEvent;
