using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Shared.IntegrationEvents.Catalog;

public record UserViewedProductIntegrationEvent(
    AccountId AccountId,
    ProductId Id
) : BaseIntegrationEvent;
