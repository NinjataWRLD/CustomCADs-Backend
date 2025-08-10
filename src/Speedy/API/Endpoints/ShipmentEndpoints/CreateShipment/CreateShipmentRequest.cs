namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.CreateShipment;

using Dtos.ShipmentContent;
using Dtos.ShipmentPayment;
using Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;
using Dtos.ShipmentSenderAndRecipient.ShipmentSender;
using Dtos.ShipmentService;

public record CreateShipmentRequest(
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
