using CustomCADs.Shared.Domain.Bases.Events;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;

public record ProductViewedApplicationEvent(
	ProductId Id,
	AccountId AccountId
) : BaseApplicationEvent;
