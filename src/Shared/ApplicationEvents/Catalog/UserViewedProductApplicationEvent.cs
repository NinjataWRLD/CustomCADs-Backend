using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Shared.ApplicationEvents.Catalog;

public record UserViewedProductApplicationEvent(
    AccountId AccountId,
    ProductId Id
) : BaseApplicationEvent;
