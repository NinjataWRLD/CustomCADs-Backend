using CustomCADs.Shared.Speedy.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Models.Shipment.Service;

namespace CustomCADs.Shared.Speedy.Models.Shipment;

public record ShipmentModel(
    ShipmentRecipientModel Recipient,
    ShipmentServiceModel Service,
    ShipmentContentModel Content,
    ShipmentPaymentModel Payment,
    ShipmentSenderModel? Sender,
    string? Id,
    string? ShipmentNote,
    string? Ref1,
    string? Ref2,
    string? ConsolidationRef,
    bool? RequireUnsuccessfulDeliveryStickerImage
);
