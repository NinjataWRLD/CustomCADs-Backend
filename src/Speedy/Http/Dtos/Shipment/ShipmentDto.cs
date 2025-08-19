namespace CustomCADs.Speedy.Http.Dtos.Shipment;

using Shipment.Delivery;
using Shipment.Primary;
using ShipmentContent;
using ShipmentPayment;
using ShipmentPrice;
using ShipmentSenderAndRecipient.ShipmentRecipient;
using ShipmentSenderAndRecipient.ShipmentSender;
using ShipmentService;

/// <param name="Id">
///     Shipment id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Sender">
///     Sender details for the shipment.
///     <br />
///     Required: Yes. See ShipmentSenderDto for details.
/// </param>
/// <param name="Recipient">
///     Recipient details for the shipment.
///     <br />
///     Required: Yes. See ShipmentRecipientDto for details.
/// </param>
/// <param name="Service">
///     Service details for the shipment.
///     <br />
///     Required: Yes. See ShipmentServiceDto for details.
/// </param>
/// <param name="Content">
///     Content details for the shipment.
///     <br />
///     Required: Yes. See ShipmentContentDto for details.
/// </param>
/// <param name="Payment">
///     Payment details for the shipment.
///     <br />
///     Required: Yes. See ShipmentPaymentDto for details.
/// </param>
/// <param name="ShipmentNote">
///     Note for the shipment, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Ref1">
///     Reference 1 for the shipment, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Ref2">
///     Reference 2 for the shipment, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Price">
///     Price details for the shipment.
///     <br />
///     Required: Yes. See ShipmentPriceDto for details.
/// </param>
/// <param name="Delivery">
///     Delivery details for the shipment.
///     <br />
///     Required: Yes. See ShipmentDeliveryDto for details.
/// </param>
/// <param name="PrimaryShipment">
///     Primary shipment details, if applicable.
///     <br />
///     Required: No. See PrimaryShipmentDto for details.
/// </param>
/// <param name="ReturnShipmentId">
///     Return shipment id, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="RedirectShipmentId">
///     Redirect shipment id, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="PendingShipment">
///     Flag indicating if the shipment is pending.
///     <br />
///     Required: Yes.
/// </param>
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
