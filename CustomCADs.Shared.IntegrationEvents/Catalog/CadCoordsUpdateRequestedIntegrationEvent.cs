using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Shared.IntegrationEvents.Catalog;

public record CadCoordsUpdateRequestedIntegrationEvent(
    CadId Id,
    UserId CreatorId,
    CoordinatesDto? CamCoordinates,
    CoordinatesDto? PanCoordinates
) : BaseIntegrationEvent;
