using CustomCADs.Speedy.Core.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Models.Shipment.Service;

namespace CustomCADs.Speedy.Core.Contracts.Shipment;

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
