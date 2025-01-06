using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Shared.IntegrationEvents.Files;

public record ProductDeletedIntegrationEvent(
    ProductId Id,
    ImageId ImageId,
    CadId CadId
) : BaseIntegrationEvent;
