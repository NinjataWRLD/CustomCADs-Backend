using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Shared.IntegrationEvents.Catalog;

public record CadPathUpdateRequestedIntegrationEvent(
    CadId Id, 
    string Path
) : BaseIntegrationEvent;
