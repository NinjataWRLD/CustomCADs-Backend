using CustomCADs.Shared.Speedy.ShipmentService.Dtos;

namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment;

public record CreateShipmentRequest(
    string UserName,
    string Password,
    Recipient Recipient,
    Service Service,
    Content Content,
    Payment Payment,
    string? Language,
    long? ClientSystemId,
    string? Id,
    string? ShipmentNote,
    string? Ref1,
    string? Ref2,
    string? ConsolidationRef,
    bool? RequireUnsuccessfulDeliveryStickerImage
);
