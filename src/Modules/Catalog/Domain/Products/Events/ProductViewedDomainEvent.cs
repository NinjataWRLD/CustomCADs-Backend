using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Domain.Products.Events;

public record ProductViewedDomainEvent(
	ProductId Id,
	AccountId AccountId
) : BaseDomainEvent;
