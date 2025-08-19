using CustomCADs.Speedy.Core.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Models.Shipment.Delivery;
using CustomCADs.Speedy.Core.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Models.Shipment.Price;
using CustomCADs.Speedy.Core.Models.Shipment.Primary;
using CustomCADs.Speedy.Core.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Models.Shipment.Service;

namespace CustomCADs.Speedy.Core.Models.Shipment;

public record ShipmentModel(
	string Id,
	ShipmentSenderModel Sender,
	ShipmentRecipientModel Recipient,
	ShipmentServiceModel Service,
	ShipmentContentModel Content,
	ShipmentPaymentModel Payment,
	string ShipmentNote,
	string Ref1,
	string Ref2,
	ShipmentPriceModel Price,
	ShipmentDeliveryModel Delivery,
	PrimaryShipmentModel PrimaryShipment,
	string ReturnShipmentId,
	string RedirectShipmentId,
	bool PendingShipment
);
