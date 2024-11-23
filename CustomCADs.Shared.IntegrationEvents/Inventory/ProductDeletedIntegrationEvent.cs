using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Shared.IntegrationEvents.Inventory;

public record ProductDeletedIntegrationEvent(ProductId Id) : BaseIntegrationEvent;
