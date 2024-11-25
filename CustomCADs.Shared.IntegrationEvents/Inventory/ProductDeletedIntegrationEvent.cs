using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Shared.IntegrationEvents.Inventory;

public record ProductDeletedIntegrationEvent(ProductId Id) : BaseIntegrationEvent;
