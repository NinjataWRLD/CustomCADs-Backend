namespace CustomCADs.Speedy.Http.Dtos.Shipment;

using Shipment.Delivery;
using Shipment.Primary;
using ShipmentContent;
using ShipmentPayment;
using ShipmentPrice;
using ShipmentSenderAndRecipient.ShipmentRecipient;
using ShipmentSenderAndRecipient.ShipmentSender;
using ShipmentService;

internal record ShipmentDto(
	string Id,
	ShipmentSenderDto Sender,
	ShipmentRecipientDto Recipient,
	ShipmentServiceDto Service,
	ShipmentContentDto Content,
	ShipmentPaymentDto Payment,
	string ShipmentNote,
	string Ref1,
	string Ref2,
	ShipmentPriceDto Price,
	ShipmentDeliveryDto Delivery,
	PrimaryShipmentDto PrimaryShipment,
	string ReturnShipmentId,
	string RedirectShipmentId,
	bool PendingShipment
);
