using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record WriteShipmentModel(
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
