using CustomCADs.Speedy.Core.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Delivery;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Primary;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Services.Shipment.Models;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment;

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
