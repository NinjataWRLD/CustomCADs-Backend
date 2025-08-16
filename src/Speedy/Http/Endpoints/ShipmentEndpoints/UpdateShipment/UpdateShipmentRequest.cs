namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.UpdateShipment;

using Dtos.ShipmentContent;
using Dtos.ShipmentPayment;
using Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;
using Dtos.ShipmentSenderAndRecipient.ShipmentSender;
using Dtos.ShipmentService;

internal record UpdateShipmentRequest(
	string Id,

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
	string? ShipmentNote,
	string? Ref1,
	string? Ref2,
	string? ConsolidationRef,
	bool? RequireUnsuccessfulDeliveryStickerImage
);
