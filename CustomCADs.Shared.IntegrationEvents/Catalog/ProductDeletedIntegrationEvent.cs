using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Shared.IntegrationEvents.Catalog;

public record ProductDeletedIntegrationEvent(ProductId Id) : BaseIntegrationEvent;
