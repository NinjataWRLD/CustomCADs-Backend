using CustomCADs.Shared.Speedy.Dtos.ShipmentContent;
using CustomCADs.Shared.Speedy.Dtos.ShipmentPayment;
using CustomCADs.Shared.Speedy.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;
using CustomCADs.Shared.Speedy.Dtos.ShipmentSenderAndRecipient.ShipmentSender;
using CustomCADs.Shared.Speedy.Dtos.ShipmentService;

namespace CustomCADs.Shared.Speedy.Services.ValidationService.ValidateShipment;

public record ValidateShipmentRequest(
    // Copied from CreateShipmentRequest
    string UserName,
    string Password,
    ShipmentRecipientDto Recipient,
    ShipmentServiceDto Service,
    ShipmentContentDto Content,
    ShipmentPaymentDto Payment,
    ShipmentSenderDto? Sender,
    string? Language,
    long? ClientSystemId,
    string? Id,
    string? ShipmentNote,
    string? Ref1,
    string? Ref2,
    string? ConsolidationRef,
    bool? RequireUnsuccessfulDeliveryStickerImage
);
