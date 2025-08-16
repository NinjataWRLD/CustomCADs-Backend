using CustomCADs.Speedy.Core.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service;

namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

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
