using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentContent;
using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentPayment;
using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentRecipient;
using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentService;

namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request;

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
